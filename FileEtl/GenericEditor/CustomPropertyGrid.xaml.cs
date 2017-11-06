using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenericEditor
{
    /// <summary>
    /// Interaction logic for CustomPropertyGrid.xaml
    /// </summary>
    public partial class CustomPropertyGrid : UserControl
    {
        public static DependencyProperty SelectedObjectProperty =
     DependencyProperty.Register("SelectedObject", typeof(object), typeof(CustomPropertyGrid),
     new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
     OnSelectedObjectChanged));

        public CustomPropertyGrid()
        {
            InitializeComponent();
        }

        public object SelectedObject
        {
            get
            {
                return (object)GetValue(SelectedObjectProperty);
            }
            set
            {
                SetValue(SelectedObjectProperty, value);
            }
        }

        protected virtual void OnSelectedObjectChanged(object Value)
        {
            if (Value != null)
            {
                Properties.Reset(Value);

                //IsLoading = true;
                await SetObject(Value);
                IsLoading = false;
            }
            else if (AcceptsNullObjects)
                Properties.Reset(null);

            SelectedObjectChanged?.Invoke(this, new EventArgs<object>(Value));
        }

        private static void OnSelectedObjectChanged(DependencyObject Object, DependencyPropertyChangedEventArgs e)
        {
            CustomPropertyGrid propertyGrid = (CustomPropertyGrid)Object;
            propertyGrid.OnSelectedObjectChanged();
        }
    }
}