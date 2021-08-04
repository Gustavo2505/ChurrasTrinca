using ChurrasTrinca.Helpers;
using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ChurrasTrinca.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingInputView : ContentView
    {
        //Propriedades repassados ao FloatingInput
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FloatingInputView), string.Empty, BindingMode.TwoWay, null);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(FloatingInputView), string.Empty, BindingMode.TwoWay, null);
        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(FloatingInputView), ReturnType.Default);
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(FloatingInputView), Keyboard.Default, coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);
        public static readonly BindableProperty IsEmailOrPhoneProperty = BindableProperty.Create(nameof(IsEmailOrPhone), typeof(bool), typeof(FloatingInputView), false, BindingMode.TwoWay);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FloatingInputView), Color.Black, BindingMode.TwoWay, null);
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(FloatingInputView), Color.Black, BindingMode.TwoWay, null);
        public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(FloatingInputView), Color.Black, BindingMode.TwoWay, null);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        public bool IsEmailOrPhone
        {
            get { return (bool)GetValue(IsEmailOrPhoneProperty); }
            set { SetValue(IsEmailOrPhoneProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public Color TitleColor
        {
            get { return (Color)GetValue(TitleColorProperty); }
            set { SetValue(TitleColorProperty, value); }
        }

        public Color PlaceHolderColor
        {
            get { return (Color)GetValue(PlaceHolderColorProperty); }
            set { SetValue(PlaceHolderColorProperty, value); }
        }

        //Propriedades da vista
        public static readonly BindableProperty ViewBorderProperty = BindableProperty.Create(nameof(ViewBorder), typeof(Border), typeof(FloatingInputView), null, BindingMode.TwoWay, null);
        public static readonly BindableProperty IconResourceNameProperty = BindableProperty.Create(nameof(IconResourceName), typeof(string), typeof(FloatingInputView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty ViewBackgroundColorProperty = BindableProperty.Create(nameof(ViewBackgroundColor), typeof(Color), typeof(FloatingInputView), Color.White, BindingMode.TwoWay);
        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(FloatingInputView), false, BindingMode.TwoWay, propertyChanged: OnShowErrorChanged);
        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(FloatingInputView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty ShowErrorMessageProperty = BindableProperty.Create(nameof(ShowErrorMessage), typeof(bool), typeof(FloatingInputView), false, BindingMode.TwoWay);

        public static readonly BindableProperty AuxCmdImgSourceProperty = BindableProperty.Create(nameof(AuxCmdImgSource), typeof(ImageSource), typeof(FloatingInputView), null, BindingMode.TwoWay, propertyChanged: OnAuxCmdImgSourceChanged);
        public static readonly BindableProperty EnablePasswordTriggerProperty = BindableProperty.Create(nameof(EnablePasswordTrigger), typeof(bool), typeof(FloatingInputView), false, BindingMode.TwoWay, propertyChanged: OnEnablePasswordTriggerChanged);

        static void OnAuxCmdImgSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as FloatingInputView;
            view.imgAuxCmd.IsVisible = true;
        }

        static void OnEnablePasswordTriggerChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as FloatingInputView;

            if (view.EnablePasswordTrigger)
            {
                EventTrigger trigger = new EventTrigger()
                {
                    Event = "Clicked"
                };

                var triggerAction = new ShowPasswordTriggerAction();
                triggerAction.ShowIcon = SvgImageSource.FromResource("ChurrasTrinca.Resources.Icons.Show.svg", vectorWidth: 30, vectorHeight: 30);
                triggerAction.HideIcon = SvgImageSource.FromResource("ChurrasTrinca.Resources.Icons.Hide.svg", vectorWidth: 30, vectorHeight: 30);
                trigger.Actions.Add(triggerAction);

                view.floatingInput.SetBinding(FloatingInput.IsPasswordProperty, new Binding(nameof(ShowPasswordTriggerAction.HidePassword), source: triggerAction));
                view.imgAuxCmd.Triggers.Add(trigger);
            }
            else
            {
                view.floatingInput.IsPassword = false;
                view.imgAuxCmd.Triggers.Clear();
            }
        }

        static void OnShowErrorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as FloatingInputView;
            if (view.HasError)
            {
                view.ViewBorder = new Border { Color = Color.Red, Thickness = 1 };
            }
            else
            {
                view.ViewBorder = null;
            }
        }

        public Border ViewBorder
        {
            get { return (Border)GetValue(ViewBorderProperty); }
            set { SetValue(ViewBorderProperty, value); }
        }

        public string IconResourceName
        {
            get { return (string)GetValue(IconResourceNameProperty); }
            set { SetValue(IconResourceNameProperty, value); }
        }

        public Color ViewBackgroundColor
        {
            get { return (Color)GetValue(ViewBackgroundColorProperty); }
            set { SetValue(ViewBackgroundColorProperty, value); }
        }

        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public bool ShowErrorMessage
        {
            get { return (bool)GetValue(ShowErrorMessageProperty); }
            set { SetValue(ShowErrorMessageProperty, value); }
        }

        public ImageSource AuxCmdImgSource
        {
            get { return (ImageSource)GetValue(AuxCmdImgSourceProperty); }
            set { SetValue(AuxCmdImgSourceProperty, value); }
        }

        public bool EnablePasswordTrigger
        {
            get { return (bool)GetValue(EnablePasswordTriggerProperty); }
            set { SetValue(EnablePasswordTriggerProperty, value); }
        }

        public FloatingInputView()
        {
            InitializeComponent();
        }

    }
}
