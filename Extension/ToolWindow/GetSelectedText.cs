﻿using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Editor;
using System;
using System.Windows;

public class GetSelectedText
{
    public IWpfTextViewHost GetCurrentViewHost()
    {
        IVsTextView vTextView = null;

        var txtMgr = (IVsTextManager)ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager));
        if (txtMgr == null) return null;

        txtMgr.GetActiveView(1, null, out vTextView);
        if (vTextView == null) return null;

        var userData = vTextView as IVsUserData;
        if (userData == null) return null;

        Guid guidIWpfTextViewHost = DefGuidList.guidIWpfTextViewHost;
        object hostObj;
        userData.GetData(ref guidIWpfTextViewHost, out hostObj);
        var viewHost = hostObj as IWpfTextViewHost;
        if (viewHost == null) return null;

        return viewHost;
    }

    ITextDocument GetTextDocumentForView(IWpfTextViewHost viewHost)
    {
        ITextDocument document;
        viewHost.TextView.TextDataModel.DocumentBuffer.Properties.TryGetProperty(typeof(ITextDocument), out document);
        return document;
    }

    public ITextSnapshot GetSelection(IWpfTextViewHost viewHost)
    {
        return viewHost.TextView.TextSnapshot;
    }
}