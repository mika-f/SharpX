// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;

namespace SharpX.CLI.Models;

internal class CompositeDisposable : IDisposable
{
    private readonly List<IDisposable> _disposables;

    public CompositeDisposable()
    {
        _disposables = new List<IDisposable>();
    }

    public void Dispose()
    {
        foreach (var disposable in _disposables)
            try
            {
                disposable.Dispose();
            }
            catch (Exception e)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                ConsoleExt.WriteError(e.Message);
            }
    }

    public void Add(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }
}