﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net;

namespace SockMgr
{
    public enum SockUnitState
	{
        //None = 0,
        Opening = 1,
        Closing = 2,
        Opened = 3,
        Closed = 4,
	}

    public class SockUnit : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static readonly string TypeListen = "listen";
        public static readonly string TypeAccept = "accept";
        public static readonly string TypeConnect = "connect";

        private string id;
        private string name;
        private string type;
        private IPEndPoint ep;
        private string title;
        private bool autorun;
        public ObservableCollection<SockUnit> Childs { get; set; }
        public byte[] SendBuff { get; set; }
        public int SendBuffSize { get; set; }
        private SockUnitState state;
        public SockUnitState State
        {
            get { return state; }
            set
            {
                state = value;
                UpdateTitle();
            }
        }

        public SockUnit()
        {
            Childs = new ObservableCollection<SockUnit>();
            SendBuff = null;
            SendBuffSize = 0;
            State = SockUnitState.Closed;
        }

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Type"));
                }
            }
        }
        public IPEndPoint EP
        {
            get { return ep; }
            set
            {
                ep = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("EP"));
                }
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }
        public bool Autorun
        {
            get { return autorun; }
            set
            {
                autorun = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Autorun"));
                }
            }
        }

        public void UpdateTitle()
        {
            if (Type == null || ID == null || EP == null)
                return;

            if (Type == SockUnit.TypeAccept)
                Title = "\tA " + EP.ToString() + " " + State;
            else if (Type == SockUnit.TypeListen && State == SockUnitState.Opening)
                Title = ID + "\tL " + EP.ToString() + "    " + "Listening";
            else if (Type == SockUnit.TypeListen && State == SockUnitState.Opened)
                Title = ID + "\tL " + EP.ToString() + "    " + "Listened";
            else if (Type == SockUnit.TypeConnect && State == SockUnitState.Opening)
                Title = ID + "\tC " + EP.ToString() + "    " + "Connecting";
            else if (Type == SockUnit.TypeConnect && State == SockUnitState.Opened)
                Title = ID + "\tC " + EP.ToString() + "    " + "Connected";
            else if (Type == SockUnit.TypeListen)
                Title = ID + "\tL " + EP.ToString() + "    " + State;
            else if (Type == SockUnit.TypeConnect)
                Title = ID + "\tC " + EP.ToString() + "    " + State;

            //if (Type == SockUnit.TypeListen)
            //    Title = ID + "\tL " + EP.ToString() + "    " + State;
            //else if (Type == SockUnit.TypeConnect)
            //    Title = ID + "\tC " + EP.ToString() + "    " + State;
            //else
            //    Title = "\tA " + EP.ToString();
        }
    }
}
