using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Diagram;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Common;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CircuitDiagram : SampleLayout 
    {
        public CircuitDiagram()
        {
            this.InitializeComponent();
            
            diagramcontrol.Constraints = diagramcontrol.Constraints & ~GraphConstraints.PanRails;
            diagramcontrol.Constraints = diagramcontrol.Constraints & ~(GraphConstraints.Zoomable|GraphConstraints.Pannable);

            diagramcontrol.Constraints |= GraphConstraints.Bridging;

            //To Disable ContextMenu
            diagramcontrol.Menu = null;

            //Initialize PageSettings and Snapping 
            InitializeDiagram();

            diagramcontrol.PointerReleased += diagramcontrol_PointerReleased;

            //Unload the diagram
            this.Unloaded += diagramcontrol_Unloaded;
        }


        private void InitializeDiagram()
        {
            if ((Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                diagramcontrol.Constraints |= GraphConstraints.AllowPan;
                diagramcontrol.Tool = Tool.ZoomPan;
            }
            else
                diagramcontrol.Tool = Tool.None;
            diagramcontrol.ScrollSettings.ScrollLimit = ScrollLimit.Diagram;
            diagramcontrol.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            diagramcontrol.PageSettings.PageBackground = new SolidColorBrush(Colors.White);
        }

        void diagramcontrol_Unloaded(object sender, RoutedEventArgs e)
        {
            diagramcontrol.PointerReleased -= diagramcontrol_PointerReleased;
            this.Unloaded -= diagramcontrol_Unloaded;
        }

        void diagramcontrol_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
    public enum Shapes
    {
        Rectangle,

        /// <summary>
        /// Star shape
        /// </summary>
        Star,

        /// <summary>
        /// Hexagon shape
        /// </summary>
        Hexagon,

        /// <summary>
        /// Octagon shape
        /// </summary>
        Octagon,

        /// <summary>
        /// Pentagon shape
        /// </summary>
        Pentagon,

        /// <summary>
        /// Heptagon shape
        /// </summary>
        Heptagon,

        /// <summary>
        /// Triangle shape
        /// </summary>
        Triangle,

        /// <summary>
        /// Ellipse shape
        /// </summary>
        Ellipse,

        /// <summary>
        /// Plus shape
        /// </summary>
        Plus,

        /// <summary>
        /// Rounded Rectangle
        /// </summary>
        RoundedRectangle,

        /// <summary>
        /// Rounded Square
        /// </summary>
        RoundedSquare,

        /// <summary>
        /// Right Triangle
        /// </summary>
        RightTriangle,

        /// <summary>
        /// ThreeDBox shape
        /// </summary>
        ThreeDBox,

        /// <summary>
        /// FlowChart Process shape
        /// </summary>
        FlowChart_Process,

        /// <summary>
        /// FlowChart Start shape
        /// </summary>
        FlowChart_Start,

        /// <summary>
        /// FlowChart Decision shape
        /// </summary>
        FlowChart_Decision,

        /// <summary>
        /// FlowChart_Predefined shape
        /// </summary>
        FlowChart_Predefined,

        /// <summary>
        /// FlowChart_StoredData shape
        /// </summary>
        FlowChart_StoredData,

        /// <summary>
        /// FlowChart_Document shape
        /// </summary>
        FlowChart_Document,

        /// <summary>
        /// FlowChart_Data shape
        /// </summary>
        FlowChart_Data,

        /// <summary>
        /// FlowChart_InternalStorage shape
        /// </summary>
        FlowChart_InternalStorage,

        /// <summary>
        /// FlowChart_PaperTape shape
        /// </summary>
        FlowChart_PaperTape,

        /// <summary>
        /// FlowChart_SequentialData shape
        /// </summary>
        FlowChart_SequentialData,

        /// <summary>
        /// FlowChart_DirectData shape
        /// </summary>
        FlowChart_DirectData,

        /// <summary>
        /// FlowChart_ManualInput shape
        /// </summary>
        FlowChart_ManualInput,

        /// <summary>
        /// FlowChart_Card shape
        /// </summary>
        FlowChart_Card,

        /// <summary>
        /// FlowChart_Delay shape
        /// </summary>
        FlowChart_Delay,

        /// <summary>
        /// FlowChart_Terminator shape
        /// </summary>
        FlowChart_Terminator,

        /// <summary>
        /// FlowChart_Display shape
        /// </summary>
        FlowChart_Display,

        /// <summary>
        /// FlowChart_LoopLimit shape
        /// </summary>
        FlowChart_LoopLimit,

        /// <summary>
        /// FlowChart_Preparation shape
        /// </summary>
        FlowChart_Preparation,

        /// <summary>
        /// FlowChart_ManualOperation shape
        /// </summary>
        FlowChart_ManualOperation,

        /// <summary>
        /// FlowChart_OffPageReference shape
        /// </summary>
        FlowChart_OffPageReference,

        /// <summary>
        /// FlowChart_Star shape
        /// </summary>
        FlowChart_Star,

        Basic,
        Composition,
        UniDirectional,
        Inherit,
        Pc,
        Gate1,
        Gate2,
        Cloud,
        AndGate,
        Diode,
        VerResistor,
        HorResistor,
        VerCapacitor,
        HorCapacitor,
        Junction,
        Battery,
        Switch,
        Coil,
        Transistor,
        Earth,
        Lamp,
        Storage,
        Power,
        None
    }


    #region HelperClass
    internal static class ShapeHelper
    {
        private static Dictionary<Shapes, string> GeometryDictionary = new Dictionary<Shapes, string>();

        static ShapeHelper()
        {
            GeometryDictionary.Add(Shapes.AndGate, "F1 M 289.461,72.4557L 286.776,72.4557L 286.776,147.629C 290.24,147.629 293.892,147.341 297.732,146.762C 301.572,146.186 305.252,145.464 308.774,144.597C 315.76,143.27 322.746,140.196 329.733,135.374C 336.717,130.554 340.99,122.598 342.55,111.514L 342.55,110.086L 342.55,108.57C 340.99,97.541 336.748,89.4011 329.82,84.1478C 322.89,78.8931 313.421,75.3438 301.413,73.4944C 298.93,73.149 296.894,72.8891 295.306,72.7158C 293.718,72.5424 291.769,72.4557 289.461,72.4557 Z M 344.889,108.57L 400.576,108.57L 400.576,111.514L 344.889,111.514C 344.253,119.828 341.814,126.512 337.57,131.564C 333.326,136.617 328.202,140.528 322.198,143.3C 316.192,146.07 309.77,147.92 302.929,148.841C 296.086,149.765 289.808,150.226 284.092,150.226L 284.092,133.598L 239.404,133.598L 239.404,130.568L 284.092,130.568L 284.092,88.8224L 239.404,88.8224L 239.404,86.485L 284.092,86.485L 284.092,69.8571C 297.429,69.8571 310.593,72.1384 323.584,76.6998C 336.574,81.261 343.676,91.885 344.889,108.57 Z ");
            GeometryDictionary.Add(Shapes.Diode, "F1 M 360.324,68.4401L 360.324,7.38678L 367.039,7.38678L 367.039,68.4401L 456.947,4.92816L 456.947,68.4401L 456.94,68.4401L 456.94,83.1815L 456.947,83.1815L 456.947,135.824L 367.039,83.1815L 367.039,142.841L 360.324,142.841L 360.324,83.1815");
            GeometryDictionary.Add(Shapes.VerResistor, "F1 M 363.62,168.975L 333.048,158.583L 363.014,146.113L 333.395,133.726L 363.014,118.917L 348.898,111.815L 348.898,84.9686L 352.535,84.9686L 352.535,109.477L 372.714,119.61L 342.143,133.381L 372.714,146.113L 342.143,158.237L 372.714,168.282L 342.143,180.753L 372.714,192.791L 341.795,207.255L 351.928,216.003L 351.928,246.226L 347.511,246.226L 347.511,217.647L 334.088,206.562L 363.014,193.138L 331.403,180.753L 363.62,168.975 Z");
            GeometryDictionary.Add(Shapes.HorResistor, "F1 M 404.969,85.1068L 415.361,54.5334L 427.833,84.5001L 440.219,54.8801L 455.028,84.5001L 462.131,70.3828L 488.976,70.3828L 488.976,74.0215L 464.469,74.0215L 454.336,94.2001L 440.565,63.6281L 427.833,94.2001L 415.708,63.6281L 405.663,94.2001L 393.192,63.6281L 381.153,94.2001L 366.689,63.2814L 357.941,73.4135L 327.719,73.4135L 327.719,68.9975L 356.297,68.9975L 367.383,55.5735L 380.807,84.5001L 393.192,52.8881L 404.969,85.1068 Z");
            GeometryDictionary.Add(Shapes.VerCapacitor, "F1 M 307.316,180.406C 335.896,171.458 364.65,171.458 393.575,180.406L 393.575,185.95C 386.3,184.564 379.428,183.294 372.963,182.14C 366.496,180.985 359.915,180.406 353.218,180.406L 353.218,246.314L 347.328,246.314L 347.328,180.406C 340.572,180.406 333.976,180.985 327.539,182.14C 321.1,183.294 314.36,184.564 307.316,185.95M 347.328,85.0543L 353.218,85.0543L 353.218,151.394L 393.575,151.394L 393.575,156.937L 307.316,156.937L 307.316,151.394L 347.328,151.394L 347.328,85.0543 Z ");
            GeometryDictionary.Add(Shapes.HorCapacitor, "F1 M 391.299,21.6218C 401.965,55.6898 401.965,89.9684 391.299,124.454L 384.689,124.454C 386.341,115.782 387.856,107.588 389.232,99.8804C 390.609,92.1724 391.299,84.3231 391.299,76.3417L 312.727,76.3417L 312.727,69.319L 391.299,69.319C 391.299,61.2657 390.609,53.4031 389.232,45.7284C 387.856,38.0538 386.341,30.0164 384.689,21.6218M 504.971,69.319L 504.971,76.3417L 425.883,76.3417L 425.883,124.454L 419.277,124.454L 419.277,21.6218L 425.883,21.6218L 425.883,69.319L 504.971,69.319 Z ");
            GeometryDictionary.Add(Shapes.Junction, "F1 M 349.019,84.9688C 370.959,84.9688 389.941,92.8208 405.962,108.525C 421.985,124.229 429.994,143.053 429.994,164.993C 429.994,187.567 421.985,206.75 405.962,222.541C 389.941,238.331 370.959,246.226 349.019,246.226C 327.369,246.226 308.575,238.331 292.639,222.541C 276.705,206.75 268.737,187.567 268.737,164.993C 268.737,143.053 276.705,124.229 292.639,108.525C 308.575,92.8208 327.369,84.9688 349.019,84.9688 Z");
            GeometryDictionary.Add(Shapes.Battery, "F1 M 391.63,180.406L 391.63,183.352L 385.827,183.352L 385.827,188.201L 382.882,188.201L 382.882,183.352L 376.82,183.352L 376.82,180.406L 382.882,180.406L 382.882,173.997L 385.827,173.997L 385.827,180.406M 364.176,158.15L 366.774,158.15L 366.774,200.76L 429.995,200.76L 429.995,203.617L 366.774,203.617L 366.774,246.226L 364.176,246.226M 328.754,183.352L 317.148,183.352L 317.148,180.406L 328.754,180.406M 268.736,200.76L 340.012,200.76L 340.012,180.406L 345.815,180.406L 345.815,223.017L 340.012,223.017L 340.012,203.617L 268.736,203.617L 268.736,200.76 Z");
            GeometryDictionary.Add(Shapes.Switch, "F1 M 388.586,48.2048C 388.613,58.2237 396.717,66.3329 406.745,66.3505C 416.764,66.3329 424.869,58.2237 424.886,48.2048C 424.869,38.1772 416.764,30.0726 406.745,30.0548C 396.717,30.0726 388.613,38.1772 388.586,48.2048 Z M 384.186,48.2048C 384.186,35.744 394.284,25.6505 406.745,25.6505C 419.188,25.6505 429.295,35.744 429.295,48.2048C 429.295,60.6569 419.188,70.7548 406.745,70.7548C 394.284,70.7548 384.186,60.6569 384.186,48.2048 Z F1 M 388.586,257.205C 388.613,267.224 396.717,275.329 406.745,275.346C 416.764,275.329 424.869,267.224 424.886,257.205C 424.869,247.177 416.764,239.073 406.745,239.055C 396.717,239.073 388.613,247.177 388.586,257.205 Z M 384.186,257.205C 384.186,244.744 394.284,234.651 406.745,234.651C 419.188,234.651 429.295,244.744 429.295,257.205C 429.295,269.657 419.188,279.755 406.745,279.755C 394.284,279.755 384.186,269.657 384.186,257.205 Z F1 M 404.523,235.205L 404.523,83.066L 380.904,106.109L 380.904,96.5169L 406.815,70.7548L 432.577,96.5169L 432.577,106.109L 409.535,83.066L 409.535,235.205L 404.523,235.205 Z");
            GeometryDictionary.Add(Shapes.Coil, "F1 M 382.478,165.944L 373.73,144.12L 375.723,144.12L 383.43,162.913L 390.878,144.12L 393.217,144.12L 384.47,165.944M 359.267,111.556C 356.15,111.093 353.926,110.863 352.599,110.863L 352.599,125.585L 366.023,136.065L 337.443,148.104L 356.583,154.86C 358.43,155.552 360.321,156.245 362.255,156.937C 364.189,157.631 366.11,158.38 368.014,159.189C 362.991,160.92 357.969,162.74 352.946,164.644C 347.922,166.549 342.986,168.312 338.137,169.927C 342.986,171.66 347.879,173.305 352.815,174.864C 357.753,176.423 362.473,178.04 366.975,179.713L 344.198,190.8L 336.403,194.524L 352.599,203.531L 352.599,219.639C 357.679,219.639 363.049,218.659 368.707,216.695C 383.43,211.383 394.053,201.279 400.578,186.383C 403.753,178.531 404.995,170.217 404.302,161.44C 403.61,152.664 401.242,144.639 397.201,137.364C 393.159,130.841 387.919,125.255 381.482,120.607C 375.043,115.959 367.639,112.941 359.267,111.556 Z M 347.143,110.516C 345.757,110.691 344.399,110.863 343.073,111.036C 341.743,111.209 340.417,111.383 339.089,111.556C 331.525,113.057 324.957,115.771 319.386,119.696C 313.813,123.624 308.863,128.503 304.534,134.333C 300.838,139.587 298.167,145.895 296.522,153.256C 294.877,160.617 294.747,167.648 296.133,174.344C 296.133,175.211 296.247,175.875 296.478,176.336C 298.267,185.576 302.05,193.557 307.825,200.284C 313.597,207.011 320.439,212.135 328.35,215.656C 330.023,216.175 331.654,216.724 333.243,217.301C 334.83,217.879 336.577,218.34 338.482,218.687C 338.943,218.86 339.435,218.976 339.954,219.033C 340.474,219.091 340.994,219.205 341.514,219.38C 342.669,219.38 344.097,219.524 345.801,219.812C 347.503,220.101 348.875,220.159 349.914,219.985L 349.914,205.263L 336.403,197.469L 330.687,194.524L 359.961,180.06L 329.735,169.927L 357.622,159.881L 359.614,158.843L 329.735,148.451L 360.653,135.372L 349.914,126.972L 349.914,110.516M 352.599,84.9693L 352.599,107.832C 353.407,108.005 354.403,108.121 355.587,108.179C 356.77,108.237 357.882,108.352 358.922,108.525C 375.203,111.181 388.194,119.467 397.894,133.38C 401.934,139.385 404.85,146.661 406.641,155.205C 408.142,164.789 407.39,174.257 404.389,183.612C 401.386,192.965 396.537,200.732 389.839,206.909C 384.815,211.471 379.187,215.165 372.951,217.993C 366.715,220.823 359.93,222.383 352.599,222.671L 352.599,246.227L 349.914,246.227L 349.914,223.017C 348.875,222.729 347.431,222.583 345.585,222.583C 343.735,222.583 342.147,222.412 340.821,222.064C 338.742,221.717 336.881,221.315 335.234,220.852C 333.589,220.391 331.87,219.899 330.082,219.38C 324.193,217.013 318.693,213.78 313.583,209.68C 308.474,205.581 304.33,200.847 301.155,195.476C 296.767,188.779 294.039,181.013 292.971,172.18C 291.902,163.347 292.725,154.888 295.439,146.805C 298.267,138.26 302.858,130.856 309.21,124.589C 315.561,118.325 322.95,113.749 331.381,110.863C 332.13,110.863 332.91,110.633 333.719,110.169C 334.931,109.708 336.634,109.248 338.829,108.784C 342.639,108.149 346.334,107.832 349.914,107.832L 349.914,84.9693L 352.599,84.9693 Z");
            GeometryDictionary.Add(Shapes.Transistor, "F1 M 341.454,133.339C 341.541,182.128 381.022,221.609 429.822,221.697C 478.622,221.609 518.103,182.128 518.19,133.339C 518.103,84.5283 478.622,45.052 429.822,44.9649C 381.022,45.052 341.541,84.5283 341.454,133.339 Z M 333.769,133.339C 333.769,80.2816 376.775,37.2807 429.822,37.2704C 482.874,37.2807 525.875,80.2816 525.88,133.339C 525.875,186.38 482.874,229.381 429.822,229.391C 376.775,229.381 333.769,186.38 333.769,133.339 Z F1 M 358.876,72.2849L 267.603,72.2849L 267.603,64.6006L 360.746,64.6006L 414.504,92.3509L 410.985,99.1796L 358.876,72.2849 Z F1 M 267.601,203.762L 267.601,196.083L 364.073,196.083L 446.976,157.175L 450.234,164.132L 365.795,203.762 F1 M 444.758,83.8008L 452.447,83.8008L 452.447,194.807L 444.758,194.807L 444.758,83.8008 Z F1 M 448.605,132.903L 547.649,132.903L 547.649,140.592L 448.605,140.592L 448.605,132.903 Z F1 M 449.961,119.831L 420.925,118.422L 391.894,117.003L 406.079,93.4282L 420.259,69.8582L 435.115,94.8473L 449.961,119.831 Z");
            GeometryDictionary.Add(Shapes.Earth, "F1 M 325.579,240.685L 373.557,240.685L 373.557,246.488L 325.579,246.488M 313.627,222.671L 385.423,222.671L 385.423,228.385L 313.627,228.385M 301.761,204.656L 397.373,204.656L 397.373,210.719L 301.761,210.719M 343.592,85.228L 355.457,85.228L 355.457,186.556L 409.585,186.556L 409.585,192.705L 289.809,192.705L 289.809,186.556L 343.592,186.556L 343.592,85.228 Z");
            GeometryDictionary.Add(Shapes.Lamp, "F1 M 366.6,214.529C 364.868,214.529 364.003,215.425 364.003,217.214C 364.003,219.177 364.868,220.159 366.6,220.159C 368.622,220.159 369.631,219.177 369.631,217.214C 369.631,215.425 368.622,214.529 366.6,214.529 Z M 359.672,217.214L 315.676,192.359L 315.676,242.07M 362.356,215.915C 363.164,213.953 364.623,212.971 366.731,212.971C 368.838,212.971 370.238,213.953 370.931,215.915L 429.908,215.915L 429.908,218.513L 370.931,218.513C 370.238,220.709 368.838,221.805 366.731,221.805C 364.623,221.805 363.164,220.709 362.356,218.513C 354.043,222.843 345.8,227.535 337.631,232.586C 329.46,237.639 321.248,242.215 312.991,246.314L 312.991,218.513L 268.65,218.513L 268.65,215.915L 312.991,215.915L 312.991,188.029L 362.356,215.915 Z");
            GeometryDictionary.Add(Shapes.Storage, "F1 M 363.552,161.18L 340.081,161.18L 340.081,170.188L 363.552,170.188M 365.976,158.409L 365.976,172.959L 337.311,172.959L 337.311,165.685L 337.311,158.409M 374.984,181.62L 374.984,184.391L 353.247,184.391L 353.247,213.489L 353.247,246.313L 350.475,246.313L 350.475,184.391L 328.649,184.391L 328.649,181.62M 353.247,85.0546L 353.247,146.976L 374.984,146.976L 374.984,149.749L 328.649,149.749L 328.649,146.976L 350.475,146.976L 350.475,85.0546L 353.247,85.0546 Z");
            GeometryDictionary.Add(Shapes.Power, "F1 M 381.324,170.62C 380.804,170.62 379.474,170.477 377.34,170.188C 375.204,169.898 373.644,169.841 372.662,170.014L 372.662,227L 379.937,226.308C 381.265,225.961 382.622,225.673 384.008,225.441C 385.394,225.21 386.924,224.836 388.598,224.314C 392.582,223.161 396.189,221.776 399.424,220.158C 402.657,218.542 405.601,216.146 408.258,212.972C 410.566,210.026 412.156,206.42 413.021,202.146C 413.886,197.872 413.742,193.83 412.589,190.021C 411.029,185.344 408.432,181.736 404.794,179.196C 401.157,176.653 397.085,174.69 392.582,173.305C 387.906,171.805 384.152,170.91 381.324,170.62 Z M 372.662,167.329C 379.937,167.502 387.184,168.586 394.401,170.578C 401.617,172.569 407.565,176.481 412.242,182.313L 429.909,182.313L 429.909,184.997L 413.628,184.997C 416.166,190.482 416.946,196.141 415.966,201.972C 415.62,203.532 415.144,205.033 414.537,206.476C 413.93,207.92 413.396,209.305 412.934,210.633L 429.909,210.633L 429.909,213.664L 411.636,213.664C 405.802,220.592 397.576,225.24 386.953,227.606C 380.601,229.281 375.058,229.945 370.324,229.598L 370.324,215.309L 354.996,215.309L 354.996,210.286L 370.324,209.94L 369.978,183.958L 354.996,183.958L 354.996,178.674L 370.324,178.674L 370.324,167.329M 301.3,168.976L 301.3,177.636L 311.953,177.636L 311.953,182.658L 287.704,182.658L 287.704,177.636L 298.702,177.636L 298.702,168.976C 297.778,168.976 296.998,169.092 296.364,169.321C 291.514,170.188 287.212,172.08 283.46,174.994C 279.706,177.91 276.674,181.36 274.366,185.344C 272.172,189.846 271.205,194.756 271.465,200.066C 271.725,205.378 273.124,210.112 275.665,214.269C 279.59,220.909 285.133,225.24 292.293,227.26C 292.466,227.433 292.769,227.549 293.202,227.606C 293.636,227.664 294.025,227.78 294.372,227.953C 294.66,228.126 295.034,228.242 295.497,228.3C 295.96,228.358 296.45,228.473 296.97,228.646C 297.144,228.646 297.388,228.704 297.706,228.818C 298.022,228.934 298.356,228.992 298.702,228.992L 298.702,218.946L 287.704,218.946L 287.704,213.664L 311.953,213.664L 311.953,218.946L 301.3,218.946L 301.3,228.992C 302.454,228.992 304.36,228.762 307.017,228.3C 311.52,227.492 315.59,225.702 319.228,222.929C 322.865,220.158 325.78,216.954 327.974,213.317C 332.305,204.253 332.536,195.246 328.668,186.297C 324.394,177.06 317.177,171.4 307.017,169.321C 305.861,169.092 303.956,168.976 301.3,168.976 Z M 301.3,151.308L 301.3,165.685C 302.281,166.032 303.336,166.202 304.461,166.202C 305.588,166.202 306.757,166.349 307.969,166.637C 316.457,168.541 323.472,173.336 329.014,181.013C 331.958,185.864 333.561,191.434 333.821,197.728C 334.08,204.021 332.796,209.65 329.966,214.616C 327.196,220.158 322.98,224.374 317.322,227.26C 312.645,230.148 307.304,231.592 301.3,231.592L 301.3,246.314L 298.702,246.314L 298.702,231.937C 298.817,232.109 297.922,231.909 296.017,231.33C 294.632,231.157 293.333,230.854 292.12,230.422C 290.908,229.988 289.637,229.513 288.309,228.992C 283.806,226.972 279.865,224.028 276.488,220.158C 273.11,216.292 270.844,211.672 269.689,206.302C 268.765,203.589 268.534,200.037 268.996,195.649C 268.996,194.726 269.226,193.398 269.689,191.665C 271.652,182.428 276.761,175.414 285.018,170.62C 288.309,168.6 291.744,167.273 295.324,166.637C 296.017,166.406 297.144,166.29 298.702,166.29L 298.702,151.308L 301.3,151.308 Z");
            GeometryDictionary.Add(Shapes.Rectangle, "M0,0 L0,1 1,1 1,0z");
            GeometryDictionary.Add(Shapes.FlowChart_Start, "  M14.5,0.5C22.232,0.5 28.5,6.768 28.5,14.5C28.5,20.5406 24.6743,25.6877 19.3137,27.6505L19.2503,27.6728L18.9902,27.7644C17.5807,28.2414 16.0706,28.5 14.5,28.5C6.768,28.5 0.5,22.232 0.5,14.5C0.5,8.58019 4.17419,3.51855 9.36655,1.47109C10.9561,0.844312 12.6878,0.5 14.5,0.5z");
            GeometryDictionary.Add(Shapes.FlowChart_Decision, "M 0,20 L 30 0 L 60,20 L 30,40 Z");
            GeometryDictionary.Add(Shapes.FlowChart_Predefined, "M 50,0 V 40 M 10,0 V 40 M 0 0 H 60 V 40 H 0 Z");
            GeometryDictionary.Add(Shapes.FlowChart_Card, "M 0 10 L 10,0 H 60 V 40 H 0 Z");
            GeometryDictionary.Add(Shapes.Ellipse, "M305.772,123.75C305.772,191.819095416645,237.434535075173,247,153.136,247C68.837464924827,247,0.5,191.819095416645,0.5,123.75C0.5,55.6809045833547,68.837464924827,0.5,153.136,0.5C237.434535075173,0.5,305.772,55.6809045833547,305.772,123.75z");
            GeometryDictionary.Add(Shapes.FlowChart_Preparation, "M 0,20 L 10,0  H 50 L 60,20 L 50,40 H10 Z");
            GeometryDictionary.Add(Shapes.RightTriangle, "M200,200L200,397.5 397.5,399.5z");
            GeometryDictionary.Add(Shapes.Star, "M 9,2 11,7 17,7 12,10 14,15 9,12 4,15 6,10 1,7 7,7 Z");
            GeometryDictionary.Add(Shapes.Basic, "M50,0 L100,50 L50,100 L0,50 z");
            GeometryDictionary.Add(Shapes.Composition, "M50,0 L100,50 L50,100 L0,50 z");
            GeometryDictionary.Add(Shapes.UniDirectional, "M0,0 L60,25 L0,50");
            GeometryDictionary.Add(Shapes.Inherit, "M2,0.5 L51.5,50 L2,99.5 L0.5,98 L0.5,2 z");
            GeometryDictionary.Add(Shapes.Cloud, "M696.37,65.0742C696.37,61.8815 694.284,59.1783 691.401,58.2474 692.442,55.9102 692.195,53.0886 690.516,50.9283 688.767,48.6745 685.942,47.7487 683.33,48.3138 683.287,45.418 681.483,42.7148 678.597,41.6706 675.755,40.6419 672.687,41.5222 670.793,43.6523 669.618,41.2944 667.185,39.6731 664.37,39.6731 661.556,39.6731 659.122,41.2944 657.947,43.6523 656.054,41.5222 652.987,40.6419 650.143,41.6706 647.259,42.7148 645.453,45.418 645.412,48.3138 642.798,47.7487 639.974,48.6745 638.225,50.9283 636.546,53.0886 636.3,55.9102 637.34,58.2474 634.456,59.1783 632.37,61.8815 632.37,65.0742 632.37,68.2668 634.456,70.9714 637.34,71.9009 636.3,74.2395 636.546,77.0599 638.225,79.2239 639.974,81.4766 642.798,82.4009 645.412,81.8347 645.453,84.7332 647.259,87.4349 650.143,88.4792 652.987,89.5053 656.054,88.6288 657.947,86.4974 659.122,88.8567 661.556,90.478 664.37,90.478 667.185,90.478 669.618,88.8567 670.793,86.4974 672.687,88.6288 675.755,89.5053 678.597,88.4792 681.483,87.4349 683.287,84.7332 683.33,81.8347 685.942,82.4009 688.767,81.4766 690.516,79.2239 692.195,77.0599 692.442,74.2395 691.401,71.9009 694.284,70.9714 696.37,68.2668 696.37,65.0742z");
            GeometryDictionary.Add(Shapes.Pc, "M24.433001,51.601002L39.202999,51.601002 39.202999,56.341999 43.396,56.341999 43.396,61.265999 20.056999,61.265999 20.056999,56.341999 24.433001,56.341999z M31.908949,38.654999C30.297743,38.654999 28.992001,39.962269 28.992001,41.573151 28.992001,43.183933 30.297743,44.489998 31.908949,44.489998 33.520157,44.489998 34.826,43.183933 34.826,41.573151 34.826,39.962269 33.520157,38.654999 31.908949,38.654999z M5.835,5.8339996L5.835,35.555996 57.983002,35.555996 57.983002,5.8339996z M0,0L64,0 64,46.677998 0,46.677998z");
            GeometryDictionary.Add(Shapes.ThreeDBox, "M 32.8333,33.3333L 127.726,33.3195L 127.538,128.09L 32.8292,127.556L 32.8333,33.3333 Z M 127.797,33.3333L 139.13,43.5L 139.13,136.853L 42.961,136.676L 32.8298,127.569L 127.601,127.965L 127.797,33.3333 Z M 127.496,128.028L 139.069,136.917");
            GeometryDictionary.Add(Shapes.Gate1, "M256.3325,267.8423L250.8335,267.8423L239.9995,267.8423L233.3335,267.8423L233.3335,302.8423L239.9995,302.8423L250.8335,302.8423L256.3325,302.8423C265.9965,302.8423,273.8325,295.0073,273.8325,285.3423C273.8325,275.6773,265.9965,267.8423,256.3325,267.8423z");
            GeometryDictionary.Add(Shapes.Gate2, "M314.6387,267.8423C309.0157,266.6363,297.6397,267.8423,297.6397,267.8423C297.6397,267.8423,303.5567,274.1753,303.5567,285.3423C303.5567,296.5093,297.6397,302.8423,297.6397,302.8423C297.6397,302.8423,309.0157,304.0483,314.6387,302.8423C333.8067,298.7313,338.1387,285.3423,338.1387,285.3423C338.1387,285.3423,333.8067,271.9533,314.6387,267.8423z");
        }

        public static Geometry ToGeometry(this Shapes source)
        {
            if (source == Shapes.None)
            {
                return null;
            }
            return Clone(GeometryDictionary[source]);
        }

        private static PathGeometry Clone(string data)
        {
            string p = "<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Data=\"" + data + "\"/>";
            Windows.UI.Xaml.Shapes.Path o = XamlReader.Load(p) as Windows.UI.Xaml.Shapes.Path;
            PathGeometry geo = Clone(o.Data as PathGeometry);
            return geo;
        }

        private static PathGeometry Clone(PathGeometry pathGeometry)
        {
            PathFigureCollection collClone = new PathFigureCollection();
            foreach (PathFigure item in pathGeometry.Figures)
            {
                PathFigure clone = new PathFigure()
                {
                    IsClosed = item.IsClosed,
                    IsFilled = item.IsFilled,
                    Segments = Clone(item.Segments),
                    StartPoint = item.StartPoint
                };
                collClone.Add(clone);
            }
            return new PathGeometry()
            {
                Figures = collClone,
                FillRule = pathGeometry.FillRule,
                Transform = pathGeometry.Transform
            };
        }

        private static PathSegmentCollection Clone(PathSegmentCollection pathSegColl)
        {
            PathSegmentCollection collClone = new PathSegmentCollection();

            foreach (PathSegment item in pathSegColl)
            {
                PathSegment clone = null;
                if (item is LineSegment)
                {
                    LineSegment seg = item as LineSegment;
                    clone = new LineSegment() { Point = seg.Point };
                }
                else if (item is PolyLineSegment)
                {
                    PolyLineSegment seg = item as PolyLineSegment;
                    clone = new PolyLineSegment() { Points = getPoints(seg.Points) };
                }
                else if (item is BezierSegment)
                {
                    BezierSegment seg = item as BezierSegment;
                    clone = new BezierSegment()
                    {
                        Point1 = seg.Point1,
                        Point2 = seg.Point2,
                        Point3 = seg.Point3
                    };
                }
                else if (item is PolyBezierSegment)
                {
                    PolyBezierSegment seg = item as PolyBezierSegment;
                    clone = new PolyBezierSegment() { Points = getPoints(seg.Points) };
                }
                else if (item is PolyQuadraticBezierSegment)
                {
                    PolyQuadraticBezierSegment seg = item as PolyQuadraticBezierSegment;
                    clone = new PolyQuadraticBezierSegment() { Points = getPoints(seg.Points) };
                }
                else if (item is QuadraticBezierSegment)
                {
                    QuadraticBezierSegment seg = item as QuadraticBezierSegment;
                    clone = new QuadraticBezierSegment() { Point1 = seg.Point1, Point2 = seg.Point2 };
                }
                else if (item is ArcSegment)
                {
                    ArcSegment seg = item as ArcSegment;
                    clone = new ArcSegment()
                    {
                        IsLargeArc = seg.IsLargeArc,
                        Point = seg.Point,
                        RotationAngle = seg.RotationAngle,
                        Size = seg.Size,
                        SweepDirection = seg.SweepDirection
                    };
                }

                collClone.Add(clone);
            }
            return collClone;
        }

        private static PointCollection getPoints(PointCollection pointCollection)
        {
            PointCollection coll = new PointCollection();
            foreach (var item in pointCollection)
            {
                coll.Add(item);
            }
            return coll;
        }

    }
    #endregion
    public static class CircuitDiagramTemplate
    {
        public const string vTemplate = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                         "<Border>" +
                                       "<TextBlock " +
                                                  " FontFamily=\"Segoe\"" +
                                                  " FontSize=\"14\"" +
                                                  " FontWeight=\"SemiBold\"" +
                                                  " Foreground=\"Black\"" +
                                                  " Text=\"{Binding Path=Content, Mode=TwoWay}\"/>" +
                                        "</Border>" +
                                       "</DataTemplate>";

    }
}
