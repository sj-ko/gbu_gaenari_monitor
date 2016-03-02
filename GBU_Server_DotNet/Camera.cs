using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GBU_Server_Monitor
{
    public class Camera : INotifyPropertyChanged
    {
        private int _camID;
        private string _camURL;

        private int _cropX;
        private int _cropY;
        private int _cropWidth;
        private int _cropHeight;

        private string _savePath;

        private string _serverPath;
        private string _configPath;

        private int _nChannel;

        public int camID
        {
            get
            {
                return _camID;
            }
            set
            {
                if (value != _camID)
                {
                    _camID = value;
                    NotifyPropertyChanged("camID");
                }
            }
        }
        public string camURL
        {
            get
            {
                return _camURL;
            }
            set
            {
                if (value != _camURL)
                {
                    _camURL = value;
                    NotifyPropertyChanged("camURL");
                }
            }
        }

        public int cropX
        {
            get
            {
                return _cropX;
            }
            set
            {
                if (value != _cropX)
                {
                    _cropX = value;
                    NotifyPropertyChanged("cropX");
                }
            }
        }

        public int cropY
        {
            get
            {
                return _cropY;
            }
            set
            {
                if (value != _cropY)
                {
                    _cropY = value;
                    NotifyPropertyChanged("cropY");
                }
            }
        }

        public int cropWidth
        {
            get
            {
                return _cropWidth;
            }
            set
            {
                if (value != _cropWidth)
                {
                    _cropWidth = value;
                    NotifyPropertyChanged("cropWidth");
                }
            }
        }

        public int cropHeight
        {
            get
            {
                return _cropHeight;
            }
            set
            {
                if (value != _cropHeight)
                {
                    _cropHeight = value;
                    NotifyPropertyChanged("cropHeight");
                }
            }
        }

        public string savePath
        {
            get
            {
                return _savePath;
            }
            set
            {
                if (value != _savePath)
                {
                    _savePath = value;
                    NotifyPropertyChanged("savePath");
                }
            }
        }

        public string serverPath
        {
            get
            {
                return _serverPath;
            }
            set
            {
                if (value != _serverPath)
                {
                    _serverPath = value;
                    NotifyPropertyChanged("serverPath");
                }
            }
        }

        public string configPath
        {
            get
            {
                return _configPath;
            }
            set
            {
                if (value != _configPath)
                {
                    _configPath = value;
                    NotifyPropertyChanged("configPath");
                }
            }
        }

        public int nChannel
        {
            get
            {
                return _nChannel;
            }
            set
            {
                if (value != _nChannel)
                {
                    _nChannel = value;
                    NotifyPropertyChanged("nChannel");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Camera()
        {
            _camID = 0;
            //_camURL = "rtsp://admin:admin@14.52.220.82/media/video1";
            _camURL = "rtsp://admin:gbudata1234@14.52.220.82:554/Streaming/Channels/101/?transportmode=unicast";

            _cropX = 0;
            _cropY = 0;
            _cropWidth = 320;
            _cropHeight = 180;
            _savePath = @"D:\anprtest";

            _serverPath = @"C:\gbu\server_160223\";
            _configPath = @"C:\gbu\cfg_160223\";

            _nChannel = 13;
        }

        public Camera(int id)
        {
            _camID = id;
            //_camURL = "rtsp://admin:admin@14.52.220.82/media/video1";
            _camURL = "rtsp://admin:gbudata1234@14.52.220.82:554/Streaming/Channels/101/?transportmode=unicast";

            _cropX = 0;
            _cropY = 0;
            _cropWidth = 320;
            _cropHeight = 180;
            _savePath = @"D:\anprtest";

            _serverPath = @"C:\gbu\server_160223\";
            _configPath = @"C:\gbu\cfg_160223\";

            _nChannel = 13;
        }

        public Camera(int id, string url)
        {
            _camID = id;
            _camURL = url;

            _cropX = 0;
            _cropY = 0;
            _cropWidth = 320;
            _cropHeight = 180;
            _savePath = @"D:\anprtest";

            _serverPath = @"C:\gbu\server_160223\";
            _configPath = @"C:\gbu\cfg_160223\";

            _nChannel = 13;
        }

    }
}
