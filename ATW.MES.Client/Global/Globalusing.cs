
#region System

global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Net.Http;
global using System.Text.Json;
global using Microsoft.Extensions.DependencyInjection;
global using CommunityToolkit.Mvvm.DependencyInjection;
global using System.Windows;

#endregion

#region ATW.CommonBase

#region CommonInterface

global using ATW.CommonBase.CommonInterface.Communicate;
global using ATW.CommonBase.CommonInterface.DataProcessing;

#endregion

#region Communicate

global using ATW.CommonBase.Communicate.HTTP;
global using ATW.CommonBase.Communicate.PLC.HSL;
global using HslCommunication;
global using HslCommunication.Profinet.Keyence;
global using HslCommunication.Profinet.Siemens;
global using HslCommunication.Profinet.Omron;

#endregion

#region DataProcessing

global using Newtonsoft.Json;
global using ATW.CommonBase.DataProcessing.DataConverter;
global using ATW.CommonBase.DataProcessing.Serializer;
global using ATW.CommonBase.DataProcessing.DataConverter.PLC;
global using ATW.CommonBase.DataProcessing.DataCheck;

#endregion

#region File


#endregion

#region Model

global using ATW.CommonBase.Model;

#endregion

#region Global

global using ATW.CommonBase.Global;

#endregion

#endregion
