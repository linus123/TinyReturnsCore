﻿namespace TinyReturns.SharedKernel.FlatFiles
{
    public interface IFlatFileIo
    {
        void OpenFile(string fileName);
        void WriteLine(string line);
        void CloseFile();
    }
}