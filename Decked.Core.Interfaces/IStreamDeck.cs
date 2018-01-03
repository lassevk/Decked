using System;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Decked.Core.Interfaces
{
    public interface IStreamDeck
    {
        [NotNull, ItemNotNull]
        Task<bool[]> GetNextKeyState();

        (int column, int row) DecodeButtonIndexToPosition(int buttonIndex);
    }
}