<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="SocialMediaSharingASP.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

<div id="Video" runat="server"></div>
  
      <div>
    <!--      <video id="Viodeoo" width="320" height="240" controls="controls" autoplay="autoplay" runat="server">
  <source id="Videoo" src="..\Images\movie.mp4" type="video/mp4"></video>
        -->
                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click1" />
      <asp:TextBox ID="tbInvoer" runat="server"></asp:TextBox>
      </div>

        

 
  Your browser does not support the video tag.
</video>


<!--
        <object  data="C:\Users\Stan\Documents\Visual Studio 2013\Projects\SocialMediaSharingASP\SocialMediaSharingASP\Images\Wildlife.wmv" type="video/x-ms-wmv" width="320" height="255</object>
         <object width="420" height="315" type="application/x-vlc-plugin" data="C:\Users\Stan\Documents\Visual Studio 2013\Projects\SocialMediaSharingASP\SocialMediaSharingASP\Images\TheFlash.mp4"></object>
        <embed src="C:\Users\Stan\Documents\Visual Studio 2013\Projects\SocialMediaSharingASP\SocialMediaSharingASP\Images\TheFlash.mp4" mce_src="C:\Users\Stan\Documents\Visual Studio 2013\Projects\SocialMediaSharingASP\SocialMediaSharingASP\Images\TheFlash.mp4"
autostart="false" />
        <video id="video1" style="width:640px; height:360px" src="TheFlash.mp4" mce_src="TheFlash.mp4"> </video> 
        --><video id="video1" style="width:640px; height:360px" src="Wildlife.wmv"> </video> 
        <!--

        <video width="320" height="240" controls="controls" autoplay="autoplay">
            <source src="C:\Users\Stan\Documents\Visual Studio 2013\Projects\SocialMediaSharingASP\SocialMediaSharingASP\Images\TheFlash.mp4" type="video/mp4">
            <object data="" width="320" height="240">
                <embed width="320" height="240" src="TheFlash.mp4">


<EMBED type="video/mp4" width="416px" height="320px" autostart="False" URL="C:\Users\Stan\Downloads\GrabIt Downloads\petition.to.remove.from.arrow.universe.121.1080-dimension.sample.mkv\TheFlash.mp4" enabled="True" balance="0" currentPosition="0" enableContextMenu="True" fullScreen="False" mute="False" playCount="1" rate="1" stretchTofit="False" uiMode="3">
<EMBED type="video/x-ms-wmv" width="416px" height="320px" autostart="False" URL="C:\Users\Stan\Documents\Visual Studio 2013\Projects\SocialMediaSharingASP\SocialMediaSharingASP\Images\WildLife.wmv" enabled="True" balance="0" currentPosition="0" enableContextMenu="True" fullScreen="False" mute="False" playCount="1" rate="1" stretchTofit="False" uiMode="3">
                -->
    </form>
</body>
</html>
