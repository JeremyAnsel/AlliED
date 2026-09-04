These files are required support files for for AlliED. You must place them in your AlliED directory, as defined in Preferences.

------------

Palette.bmp is what AlliED uses to draw the icons for the map, while the text files contain the text of wavs included with XWA, and are used by the WAV selector in AlliED.

------------

The CustomWAVs.txt file enables you to add you own wavs if required, or just reorganise the one ones included with the game, as I've done with the default CustomWAVs file. XWA searches for wavs in the XWingAlliance\Wave directory, first on your harddrive, then, if it doesn't find it there, on the CD. So you will need to your wavs into the \Wave directory on your harddrive. You may also create subdirectories within the \Wave directory to put your wavs e.g. XWingAlliance\Wave\MyWavs 

The format for these files are:

[wav name] [space] [IFF number] [space] [text of message] 

The wav name must NOT have the .wav extension, and must include the path from XWA's Wave directory to the wav. 
The text will be displayed in the IFF color: 0 = green, 1 or 4 = red, 2 = yellow 3 = yellow, 5 = purple. 
A line beginning with a space will be displayed as is, in white font. 

E.g. if you have a wav called StopShuttle.wav in the \Wave directory, which is a Rebel voice saying "Stop the Shuttle!" then add to the CustomWAVs.txt file the line: 

StopShuttle 0 Stop the Shuttle! 

If that wav is in the subdirectory \MyWavs you will need to add: 

MyWavs\StopShuttle 0 Stop the Shuttle!

----------

ShipSeq.txt stores the order of craft as displayed in the Ship list. To edit the sequence, choose "Shiplist Sequence" from the "Tools" menu. This will bring up a window which will allow you to multi-select and move the slots. There is also a button to insert a blank line for separating groups. 

----------

Speeds.txt contains the list of all craft speeds used to calculate the times between WPs on the map. This list can be edited from the "ShipList Sequence" window under the Tools menu.

