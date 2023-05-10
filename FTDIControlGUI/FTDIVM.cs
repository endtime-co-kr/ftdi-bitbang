using FTD2XX_NET;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FTDIControlGUI
{
    public class FTDIVM : BaseViewModel
    {
        private static byte[] _bitMask = new byte[4];
        private static byte[] _bitOutput = new byte[4];
        private int _deviceId;
        private int _portNum;
        private FTDI _ftdi;

        public int PortNumber
        {
            get => _portNum;
        }

        public int DeviceId
        {
            get => _deviceId;
        }

        private bool _isOutput;
        public bool IsOutput 
        { 
            get => _isOutput;
            set
            {
                _isOutput = value;
                OnPropertyChanged(nameof(IsOutput));
                ChangePortMode();
            }
        }

        public string readVal = "";
        public string ReadVal
        {
            get => readVal;
            set
            {
                readVal = value;
                OnPropertyChanged(nameof(ReadVal));
            }
        }

        private bool _isHigh;
        public bool IsHigh 
        { 
            get => _isHigh;
            set
            {
                _isHigh = value;
                OnPropertyChanged(nameof(IsHigh));
                ChangePortValue();
            }
        }

        public FTDIVM(ref FTDI ftdi, int index, int portNum)
        {
            _ftdi = ftdi;
            _deviceId = index;
            _portNum = portNum;
        }

        private void ChangePortMode()
        {
            if (_isOutput)
            {
                _bitMask[_deviceId] |= (byte)(0x01 << _portNum);
            }
            else
            {
                _bitMask[_deviceId] &= (byte)(~(0x01 << _portNum));
            }
            _ftdi.SetBitMode(_bitMask[_deviceId], 0x01);

            byte[] data_out = new byte[8];
            uint readByte = 0;
            
            _ftdi.Read(data_out, 1, ref readByte);
            ReadVal = data_out[0].ToString("X");
        }

        private void ChangePortValue()
        {
            uint numWriten = 0;

            if (!_isHigh)
            {
                _bitOutput[_deviceId] &= (byte)(~(0x01 << _portNum));
            }
            else
            {
                _bitOutput[_deviceId] |= (byte)((0x01 << _portNum));
            }
            var r = _ftdi.Write(new byte[] { _bitOutput[_deviceId] }, 1, ref numWriten);

            //_ftdi.SetBitMode(_bitMask[_deviceId], 0x04);

            //data_out[0] = (byte)(0x01 << _portNum);
            //data_out[1] = (byte)(0x01 << _portNum);
            //data_out[2] = (byte)(0x01 << _portNum);
            //data_out[3] = (byte)(0x01 << _portNum);
            //data_out[4] = (byte)(0x01 << _portNum);
            //data_out[5] = (byte)(0x01 << _portNum);
            //data_out[6] = (byte)(0x01 << _portNum);
            //data_out[7] = (byte)(0x01 << _portNum);


            byte[] data_out = new byte[8];
            uint readByte = 0;
            _ftdi.Read(data_out, 1, ref readByte);
            ReadVal = data_out[0].ToString("X");

            

            //uint waiting = 0;

            //do
            //{
            //    _ftdi.GetTxBytesWaiting(ref waiting);
            //} while (waiting > 0);

            //if (r == FTDI.FT_STATUS.FT_OK)
            //{
            //    Console.WriteLine("write ok");
            //}
        }
    }
}
