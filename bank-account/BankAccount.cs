using System;


public class BankAccount
{
    readonly object _monitor = new object();
    float _balance;
    bool _open;

    public float Balance
        => SyncChecked(() => _balance);

    public void Open()
        => Sync(() => _open = true);

    public void Close()
        => Sync(() => _open = false);

    public void UpdateBalance(float change)
        => SyncChecked(() => _balance += change);

    T Sync<T>(Func<T> method)
    {
        lock (_monitor)
        {
            return method();
        }
    }

    T SyncChecked<T>(Func<T> method)
        => Sync<T>(() => Checked(method));

    T Checked<T>(Func<T> method)
    {
        if ( ! _open)
        {
            throw new InvalidOperationException("The account is closed.");
        }

        return method();
    }
}
