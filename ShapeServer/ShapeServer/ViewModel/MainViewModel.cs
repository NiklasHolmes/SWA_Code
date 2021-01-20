using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ShapeServer.Communication;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace ShapeServer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<BaseShape> Items { get; set; }
        public ObservableCollection<string> Shapes { get; set; }

        private int iWidth;
        public int IWidth
        {
            get { return iWidth; }
            set { iWidth = value; RaisePropertyChanged(); }
        }
        private int iHeight;
        public int IHeight
        {
            get { return iHeight; }
            set { iHeight = value; RaisePropertyChanged(); }
        }
        private int iYValue;
        public int IYValue
        {
            get { return iYValue; }
            set { iYValue = value; RaisePropertyChanged(); }
        }
        private string selectedShape;
        public string SelectedShape
        {
            get { return selectedShape; }
            set { selectedShape = value; RaisePropertyChanged(); }
        }
        private int iXValue;
        public int IXValue
        {
            get { return iXValue; }
            set { iXValue = value; RaisePropertyChanged(); }
        }

        private string visibilityBool;

        public string VisibilityBool
        {
            get { return visibilityBool; }
            set { visibilityBool = value; RaisePropertyChanged(); }
        }

        public RelayCommand AddBtnClickCmd { get; set; }
        public RelayCommand ActAsServerClickCmd { get; set; }
        public RelayCommand ActAsClientClickCmd { get; set; }

        public bool IsConnected { get; set; }
        public bool IsServer { get; set; }

        // Kommunikation: 
        private Communicator com;
        private const int port = 10100;
        private const string ip = "127.0.0.1";


        public MainViewModel()
        {
            VisibilityBool = "Visible";
            IsServer = false;
            SelectedShape = " ";
            IXValue = 100;
            IYValue = 100;

            Items = new ObservableCollection<BaseShape>();

            GenerateTestShapes();

            Shapes = new ObservableCollection<string>()
            {
                " ",
                "Quadrat",
                "Rechteck",
                "Kreis",
                "Ellipse"
            };

            ActAsServerClickCmd = new RelayCommand(
                () => {
                    // neuen Server anlegen:
                    com = new Communicator(true, GuiUpdate);
                    IsServer = true;
                    IsConnected = true;
                },      // Action what to do after button clicked
                () => { return !IsConnected; }      // Button klickbar?
            );

            ActAsClientClickCmd = new RelayCommand(
                () => {
                    // neuen Client anlegen:
                    com = new Communicator(false, GuiUpdate);
                    IsServer = false;
                    IsConnected = true;
                    VisibilityBool = "Hidden";

                },      // Action what to do after button clicked
                () => { return !IsConnected; }      // Button klickbar?
            );

            AddBtnClickCmd = new RelayCommand(() =>
            {
                string ShapeData = (SelectedShape + "|" + IYValue + "|" + IXValue + "|" + IWidth + "|" + IHeight);
                com.Send(Encoding.UTF8.GetBytes(ShapeData));
                GuiUpdate(ShapeData);
            }, () =>
            {
                return (SelectedShape != " " && IWidth > 0 && IsServer);
            }
            );
        }


        private void GuiUpdate(string shapeMessage)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                string[] splitMessage = shapeMessage.Split('|');
                string shapeType = splitMessage[0];
                Double shapeTop = Double.Parse(splitMessage[1]);
                Double shapeLeft = Double.Parse(splitMessage[2]);
                int shapeWidth = Int32.Parse(splitMessage[3]);
                int shapeHeight = Int32.Parse(splitMessage[4]);

                switch (shapeType)
                {
                    case "Kreis":
                        Items.Add(new CircleVM { Top = shapeTop, Left = shapeLeft, EllipseHeight = shapeWidth, EllipseWidth = shapeWidth });
                        break;
                    case "Ellipse":
                        Items.Add(new CircleVM { Top = shapeTop, Left = shapeLeft, EllipseHeight = shapeHeight, EllipseWidth = shapeWidth });
                        break;
                    case "Rechteck":
                        Items.Add(new RectangleVM { Top = shapeTop, Left = shapeLeft, RectangleHeight = shapeHeight, RectangleWidth = shapeWidth });
                        break;
                    case "Quadrat":
                        Items.Add(new RectangleVM { Top = shapeTop, Left = shapeLeft, RectangleHeight = shapeWidth, RectangleWidth = shapeWidth });
                        break;
                    default:
                        break;
                }
            });
        }

        private void GenerateTestShapes()
        {
            Items.Add(new CircleVM { Top = 200.0, Left = 300.0, EllipseHeight = 40, EllipseWidth = 40 });
            Items.Add(new CircleVM { Top = 200.0, Left = 150.0, EllipseHeight = 80, EllipseWidth = 40 });
            Items.Add(new RectangleVM { Top = 300.0, Left = 400.0, RectangleHeight = 40, RectangleWidth = 80 });
            Items.Add(new RectangleVM { Top = 50.0, Left = 50.0, RectangleHeight = 70, RectangleWidth = 70 });
        }


    }
}