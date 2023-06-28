using FTD2XX_NET;
using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static FTD2XX_NET.FTDI;

namespace FTDIControlGUI
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<FTDIVM> _ftdiCollection;
        public ObservableCollection<FTDIVM> FtdiCollection 
        { 
            get => _ftdiCollection;
            set
            {
                _ftdiCollection = value;
                OnPropertyChanged(nameof(FtdiCollection));
            }
        }

        private string _runMessage;
        public string RunMessage
        {
            get => _runMessage;
            set { _runMessage = value; OnPropertyChanged(nameof(RunMessage)); }
        }


        private ICommand _commandOpen;
        public RelayCommand<object> CommandOpen
        {
            get
            {
                _commandOpen ??= new RelayCommand<object>(OnClickedOpenButton);
                return (RelayCommand<object>)_commandOpen;
            }
        }

        private void OnClickedOpenButton(object obj)
        {
            
            Task.Run(() =>
            {
                FT_DEVICE_INFO_NODE[] devicelist = new FT_DEVICE_INFO_NODE[10];
                (new FTDI()).GetDeviceList(devicelist);

                for (int i = 0; i < devicelist.Length; i++)
                {
                    if (devicelist[i] != null)
                    {
                        FTDI ftdi = new FTDI();
                        var status = ftdi.OpenByIndex((uint)i);
                        if (status != FTDI.FT_STATUS.FT_OK)
                        {
                            //MessageBox.Show($"open error {1}");
                            //ftdi.SetBitMode(0x00, 0x00);
                            Application.Current.Dispatcher.Invoke(() =>
                            RunMessage = $"retry (open) {i}");
                            i--;
                            continue;
                        }
                        Application.Current.Dispatcher.Invoke(() =>
                           RunMessage = $"open {i}");

                        status = ftdi.SetBaudRate(62500);
                        if (status != FTDI.FT_STATUS.FT_OK)
                        {
                            //MessageBox.Show("SetBaudRate error");
                        }
                        status = ftdi.SetLatency(16);
                        if (status != FTDI.FT_STATUS.FT_OK)
                        {
                            //MessageBox.Show("SetLatency error");
                        }
                        status = ftdi.SetTimeouts(5000, 5000);
                        if (status != FTDI.FT_STATUS.FT_OK)
                        {
                            //MessageBox.Show("SetTimeouts error");
                        }
                        //status = _ftdi.SetCharacters(, false, "", false);
                        status = ftdi.SetFlowControl(0, 0x11, 0x13);
                        if (status != FTDI.FT_STATUS.FT_OK)
                        {
                            //MessageBox.Show("SetFlowControl error");
                        }
                        for (int j = 0; j < 8; j++)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            FtdiCollection.Add(new FTDIVM(ref ftdi, i, j) { IsOutput = true, IsHigh = false })
                            );
                        }
                        Thread.Sleep(1000);


                    }
                }

                while (true)
                {
                    var r = from x in FtdiCollection where x.ReadVal != "0" select x;
                    if (r.Count() == 0)
                    {
                        //MessageBox.Show("Init complete");
                        Application.Current.Dispatcher.Invoke(() => RunMessage = $"init done");
                        break;
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(() => RunMessage = $"init.. remind ({r.Count()})");
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private FTDI[] _ftdiA;

        
        public MainViewModel()
        {
            FtdiCollection = new ObservableCollection<FTDIVM>();
            
            //FtdiCollection.Add(new FTDIVM() { IsOutput = true, IsHigh = false });
            //FtdiCollection.Add(new FTDIVM() { IsOutput = false, IsHigh = false });
            //FtdiCollection.Add(new FTDIVM() { IsOutput = true, IsHigh = true });
            //FtdiCollection.Add(new FTDIVM() { IsOutput = false, IsHigh = true });
            //FtdiCollection.Add(new FTDIVM() { IsOutput = true, IsHigh = true });
        }

    }
}
