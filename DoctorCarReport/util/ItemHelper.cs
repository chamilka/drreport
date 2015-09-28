using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DoctorCarReport
{
    public class ItemHelper : DependencyObject
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached("IsChecked", typeof(bool?), typeof(ItemHelper), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCheckedPropertyChanged)));
        private static void OnIsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Organization && ((bool?)e.NewValue).HasValue)
                foreach (Car p in (d as Organization).Cars)
                    ItemHelper.SetIsChecked(p, (bool?)e.NewValue);

            if (d is Car)
            {
                int checkedVal = ((d as Car).GetValue(ItemHelper.ParentProperty) as Organization).Cars.Where(x => ItemHelper.GetIsChecked(x) == true).Count();
                int uncheckedVal = ((d as Car).GetValue(ItemHelper.ParentProperty) as Organization).Cars.Where(x => ItemHelper.GetIsChecked(x) == false).Count();
                if (uncheckedVal > 0 && checkedVal > 0)
                {
                    ItemHelper.SetIsChecked((d as Car).GetValue(ItemHelper.ParentProperty) as DependencyObject, null);
                    return;
                }
                if (checkedVal > 0)
                {
                    ItemHelper.SetIsChecked((d as Car).GetValue(ItemHelper.ParentProperty) as DependencyObject, true);
                    return;
                }
                ItemHelper.SetIsChecked((d as Car).GetValue(ItemHelper.ParentProperty) as DependencyObject, false);
            }
        }
        public static void SetIsChecked(DependencyObject element, bool? IsChecked)
        {
            element.SetValue(ItemHelper.IsCheckedProperty, IsChecked);
        }
        public static bool? GetIsChecked(DependencyObject element)
        {
            return (bool?)element.GetValue(ItemHelper.IsCheckedProperty);
        }

        public static readonly DependencyProperty ParentProperty = DependencyProperty.RegisterAttached("Parent", typeof(object), typeof(ItemHelper));
        public static void SetParent(DependencyObject element, object Parent)
        {
            element.SetValue(ItemHelper.ParentProperty, Parent);
        }
        public static object GetParent(DependencyObject element)
        {
            return (object)element.GetValue(ItemHelper.ParentProperty);
        }
    }
}
