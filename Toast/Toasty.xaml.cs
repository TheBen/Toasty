using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Toast {
    public partial class Toasty : UserControl, ICommandSource {
        #region Consts

        private static readonly TimeSpan ANIMATION_TIME = TimeSpan.FromMilliseconds(300);

        #endregion

        #region Data Members

        private Storyboard m_ShowAnimation;

        #endregion

        #region Properties

        #region Message

        public string Message {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(Toasty), new PropertyMetadata(string.Empty, OnMessageChanged, OnMessageSet));

        #endregion

        #region CommandTitle

        public string CommandTitle {
            get { return (string)GetValue(CommandTitleProperty); }
            set { SetValue(CommandTitleProperty, value); }
        }

        public static readonly DependencyProperty CommandTitleProperty =
            DependencyProperty.Register("CommandTitle", typeof(string), typeof(Toasty));

        #endregion

        #region Duration

        public TimeSpan Duration {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(Toasty), new PropertyMetadata(TimeSpan.FromSeconds(1)));

        #endregion

        #region Command

        /// <summary>
        /// Get or set the Command property
        /// </summary>
        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Reflects the parameter to pass to the CommandProperty upon execution.
        /// </summary>
        public object CommandParameter {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        ///     The target element on which to fire the command.
        /// </summary>
        public IInputElement CommandTarget {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = Button.CommandProperty.AddOwner(typeof(Toasty));

        public static readonly DependencyProperty CommandParameterProperty = Button.CommandParameterProperty.AddOwner(typeof(Toasty));

        public static readonly DependencyProperty CommandTargetProperty = Button.CommandTargetProperty.AddOwner(typeof(Toasty));

        #endregion

        #region MessageColor

        public Brush MessageColor {
            get { return (Brush)GetValue(MessageColorProperty); }
            set { SetValue(MessageColorProperty, value); }
        }

        public static readonly DependencyProperty MessageColorProperty =
            DependencyProperty.Register("MessageColor", typeof(Brush), typeof(Toasty), new PropertyMetadata(Brushes.White));

        #endregion

        #region ActionColor

        public Brush ActionColor {
            get { return (Brush)GetValue(ActionColorProperty); }
            set { SetValue(ActionColorProperty, value); }
        }

        public static readonly DependencyProperty ActionColorProperty =
            DependencyProperty.Register("ActionColor", typeof(Brush), typeof(Toasty), new PropertyMetadata(Brushes.White));

        #endregion

        #endregion

        #region C'tor

        public Toasty() {
            InitializeComponent();

            this.Loaded += Toasty_Loaded;
        }

        private void Toasty_Loaded(object sender, RoutedEventArgs e) {
            this.Loaded -= Toasty_Loaded;

            Parent.SetValue(UIElement.ClipToBoundsProperty, true);

            // Move the toast below the parent
            ToastRoot.RenderTransform = new TranslateTransform(0, Height);

            InitializeAnimation();
        }

        #endregion

        #region Public Methods

        public void Show() {
            InitializeAnimation();

            if(!string.IsNullOrEmpty(Message) && !string.IsNullOrWhiteSpace(Message)) {
                m_ShowAnimation.Begin();
            }
        }

        public void Show(string message) {
            Message = message;
        }

        public void Show(string message, TimeSpan duration) {
            Duration = duration;
            Show(message);
        }

        #endregion

        #region Event Handlers

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Toasty toast = d as Toasty;
            if(toast == null) {
                return;
            }

            toast.Show();
        }


        private static object OnMessageSet(DependencyObject d, object baseValue) {
            Toasty toast = d as Toasty;
            if(toast != null) {
                toast.Show();
            }

            return baseValue;
        }

        #endregion

        #region Private Methods

        private void InitializeAnimation() {
            // Half duration is needed because of AutoReverse
            var halfDuration = TimeSpan.FromTicks(Duration.Ticks / 2);

            DoubleAnimationUsingKeyFrames showAnim = new DoubleAnimationUsingKeyFrames() {
                AutoReverse = true,
                Duration = new Duration(halfDuration)
            };

            showAnim.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)), Value = Height });
            showAnim.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(ANIMATION_TIME), Value = 0 });

            Storyboard.SetTarget(showAnim, ToastRoot);
            Storyboard.SetTargetProperty(showAnim, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));

            m_ShowAnimation = new Storyboard();
            m_ShowAnimation.Children.Add(showAnim);
        }

        #endregion
    }
}