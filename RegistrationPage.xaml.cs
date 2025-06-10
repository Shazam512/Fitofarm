using Microsoft.Maui.Controls;
using System;
using System.Linq;

// Добавьте пространство имен, где объявлен AdminPage
using P3;

namespace P3;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
        this.Appearing += OnPageAppearing;
    }

    private void OnPageAppearing(object sender, EventArgs e)
    {
        LoginEntry.Focus();
    }

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        var entry = sender as Entry;
        entry?.Focus();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var login = LoginEntry.Text;
        var phone = PhoneEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
            return;
        }

        if (!IsValidPhoneNumber(phone))
        {
            await DisplayAlert("Ошибка", "Номер должен начинаться с +7 и содержать 10 цифр.", "OK");
            return;
        }

        if (!IsValidPassword(password))
        {
            await DisplayAlert("Ошибка", "Пароль должен содержать минимум 8 символов, включая заглавные буквы, цифры и специальные символы.", "OK");
            return;
        }

        Preferences.Set("User  Login", login);
        Preferences.Set("User  Phone", phone);
        Preferences.Set("User  Password", password);

        await DisplayAlert("Успех", "Регистрация прошла успешно!", "OK");

        await Navigation.PushAsync(new ProfilePage());
    }

    public bool IsValidPhoneNumber(string phone)
    {
        return phone.StartsWith("+7") && phone.Length == 12 && phone.Skip(2).All(char.IsDigit);
    }

    public bool IsValidPassword(string password)
    {
        return password.Length >= 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsDigit) &&
               password.Any(ch => !char.IsLetterOrDigit(ch));
    }
}