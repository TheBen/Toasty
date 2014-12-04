Toasty
======

A toast control for WPF like the Snackbar toast in Android.

[Available on NuGet!](https://www.nuget.org/packages/Toasty/)

![Alt text](https://i.imgur.com/zbcWHXQ.jpg)

### How To ###

``` XAML
<toast:Toasty Height="30"
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
