using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncLoadItems
{
    //see:  https://msdn.microsoft.com/en-us/magazine/dn630647.aspx

    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}