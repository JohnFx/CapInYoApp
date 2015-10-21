========================
=Bust A Cap in Your App=
========================

Purpose:
This utility runs in the background, with a system tray icon. Its whole purpose is to automatically enable caps lock when a user specified program is activated, and turn it back off when that program loses focus. 

Background:
It was created originally for use by an architect that needed caps-lock enabled whenever working in Autocad or Revit, but was annoyed by having to remember to turn it back off when working in other apps.


Setup:
(1) Copy the Exe from the zip file to your computer.

(2) Make a shortcut to that exe and put it in your "All Programs...Startup" folder startup folder.

(3) Edit the "Target" value of the shortcut to add a command line parameter for the string that is contained in the apps exe name.
Example for Revit: C:\BusACap\BustACapInYourApp.exe Revit


Usage Notes:
While it is running it will do the following:
* When an app with the value specified in setup step 3 gets focus caps lock will go on and a bubble from the gun icon in the system tray will alert you to the change.

* When that app loses focus, it will turn caps lock back off and notify you again.

* You can turn it off by right clicking the gun icon and selecting "exit".



