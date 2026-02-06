#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.GroupBar
{
    public class NoteViewModel
    {
        private ObservableCollection<Note> notes;

        public ObservableCollection<Note> Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public NoteViewModel()
        {
            notes = GetNotes();
        }
        private ObservableCollection<Note> GetNotes()
        {
            ObservableCollection<Note> _notes = new ObservableCollection<Note>();
            _notes.Add(new Note() { Message = "Meeting with robb at 7PM" });
            _notes.Add(new Note() { Message = "Package delivered on monday" });
            _notes.Add(new Note() { Message = "Due at tuesday" });
            _notes.Add(new Note() { Message = "License need to be renewed" });
            return _notes;
        }
    }
}
