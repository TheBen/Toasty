Toasty
======

A toast control for WPF like the Snackbar toast in Android.

[Available on NuGet!](https://www.nuget.org/packages/Toasty/)

![Alt text](https://i.imgur.com/zbcWHXQ.jpg)

### How To ###
Reference the assembly:
``` XAML
xmlns:toasty=clr-namespace:Toasty;assembly=Toasty
```

And just add a a ``Toasty`` object to your main ``Grid``:
``` XAML
<toast:Toasty x:Name="myToast"
		Height="30"
        	VerticalAlignment="Bottom"
        	Duration="0:0:0:2"
		Message="{Binding MyMessage}"
		CommandTitle="myAction"
		Command="{Binding MyCommand}"
		CommandParameter="someParam"
		FontFamily="Segoe UI"
		FontWeight="Bold"
		MessageColor="Orange"
		ActionColor="Yellow"
		Background="Black"
		FontSize="12" />
```

Showing a toast can be code programatically:
``` C#
myToast.Show();
```

Or if you use MVVM, just bind the ``Message`` property of a ``Toasty`` to a string like so:
``` XAML
<toast:Toasty Message={Binding MyMessage} />
```
Setting a new string to ``MyMessage`` will show a toast.


Feel free to contact me with any issues, requests or feedback you may have.
I'm open for suggestions and improvemnts and encourage you to fork the project.
