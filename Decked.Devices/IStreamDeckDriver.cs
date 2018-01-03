using System;
using System.Threading.Tasks;

namespace Decked.Devices
{
    public interface IStreamDeckDriver
    {
        Task<StreamDeckKeyEvent> GetNextKeyEvent();
    }
}