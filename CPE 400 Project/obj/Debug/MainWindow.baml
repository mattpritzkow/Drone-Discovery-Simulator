   M S B A M L   `   `   `  ����J  FCPE 400 Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CPE400Project.MapDisplayCPE400Project.MapDisplay  5   	       CPE400Project.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:CPE400Project.MapDisplay     5   	     � Title$  
MainWindow��5   	   $
��1600��5	   	   $	��900��5
   	    � WindowState$ 	Maximized=�5   	   .��5      �� -   5         ��Name$ 	MainPanel��5   ,   $��Center=�6   .+�5   
   �� y�6   ��  �� Path$ MapVis� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.+�5      )   #CPE400Project.MapDisplay.MapElement  -   5          Name$ MapGrid��5       $��0��6J   
   Map 61   �� $	 Map�6J   �� 5      $��30��6   
������� �� Orientation$ 
Horizontal=�6=   $��Center=�6V   $��Center=�6s   $	��0 5q�6�   .+�5      �� $
��12pt(�6   $��KEY |��6,   $��Center=�69   $	��2 0q�6T   �� 5      $	��7px��6   $	��7px��6(   
���  ��$��Center=�6F   $��Center=�6a   $	��2 0q�6~   �� 5      $��Center=�6   $��Center=�67   $	��2 0q�6T   $��	Home Base��6a   �� 5      $	��4px��6   $	��4px��6(   
���   �$��Center=�6H   $��Center=�6c   $	��2 0q�6�   �� 5      $��Center=�6   $��Center=�67   $	��2 0q�6T   $��Drone��6a   5      �� 5      -   5        	 ��Name$	 
restartBtn��5      $y�	Collapsed=�6)   $	��150��6@   .��6a   Restart Simulation6u   5      �� 5   
   -   5        $ LoadingScreen��5      $��Center=�6�   $��Center=�6�   y�6,   �� $ 
LoadingVis�$ TwoWay=�$ PropertyChanged=�.+�5      �� $��Generating Map...��6   $
��25pt(�61   5      �� 5   
   -   5        $ OptionsMenu��5   �   $��Center=�6n   y�6   �� $ 
OptionsVis�$ TwoWay=�$ PropertyChanged=�.+�5      �� $��Center=�6   $
��20pt(�65   $��0 0 0 10q�6E   $��Region Mapping Simulation��6W   6z   � 5       
 � ColumnDefinitions
 5!      �� 5"      �� 5#      $
��auto �6'   �� 5$      $
��auto �6'   �� 5%      $
��auto �6'   �� 5&      .+�5(      �� $��Right=�6   $��1��69   $��Grid Size (Pixels): ��6I   $��18��6e   $	��Top=�6q   6�   �� 5)      $��2��6   $��50��6*   $	��2 0q�6�   ��65   �� $ MapWidth�$ TwoWay=�$ PropertyChanged=�6�   �� 5*      $��3��6   $��50��6*   $	��2 0q�6�   ��65   �� $ 	MapHeight�$ TwoWay=�$ PropertyChanged=�6�   5+      � 5,      
 5-      �� 5.      �� 5/      $
��auto �6'   �� 50      $
��auto �6'   �� 51      $
��auto �6'   �� 52      .+�54      �� $��Right=�6   $��1��69   $��Drone Vision Radius: ��6I   $��18��6f   $	��Top=�6r   6�   �� 55      -   5        $��VisionSlider��55       �� TickFrequency$ 1Q�6-    �� IsSnapToTickEnabled . $��2��6�   $	��100��6�    � Minimum$ 12Q�6�    � Maximum$ 50Q�6�   $
��Left=�6�    � Value 6Z   �� $ DroneVision�$ TwoWay=�$ PropertyChanged=�6  �� 56      $��35��6   $��
5, 0, 0 ,0q�6'   $��3��6;   $
��Left=�6�   ��6K   ��  �� ElementName$ VisionSlider��$ Value�6�   57      � 58      
 59      �� 5:      �� 5;      $
��auto �6'   �� 5<      $
��auto �6'   �� 5=      $
��auto �6'   �� 5>      .+�5@      �� $��Right=�6   $��1��69   $��Number of Drones: ��6I   $��18��6c   $	��Top=�6o   6�   �� 5A      -   5        $��DroneSlider��5A      $ 1Q�6,    . $��2��6�   $	��100��6�   $ 5Q�6�   $ 25Q�6�   $
��Left=�6�    6Y   �� $ 	NumDrones�$ TwoWay=�$ PropertyChanged=�6�   �� 5B      $��35��6   $��
5, 0, 0 ,0q�6'   $��3��6;   $
��Left=�6�   ��6K   �� $ DroneSlider��$ Value�6�   5C      � 5D      
 5E      �� 5F      $
��143* �6'   �� 5G      $
��133* �6'   �� 5H      $
��auto �6'   �� 5I      $
��auto �6'   �� 5J      $
��auto �6'   �� 5K      $
��276* �6'   .+�5M      �� $	��105��6   $��Right=�6(   $��2��6D   $��Drone Battery Life: ��6T   6r   �� 5N      -   5        $��BatterySlider��5N      $ 1Q�6.    . $��3��6�   $	��100��6�   $
 2000Q�6�   $ 20000Q�6�   $
��Left=�6�    6[   �� $ DroneBattery�$ TwoWay=�$ PropertyChanged=�6	  �� 5O      $��35��6   $��
5, 0, 0 ,0q�6'   $��4��6;   $
��Left=�6�   ��6K   �� $ BatterySlider��$ Value�6�   5P      �� 5Q      -	   5        $��100px��5Q   .   .��6<   Run Simulation6L   5R      5T       � 	Resources 5V       �� 
TargetType �� Property �� Value��%S   '�       �� 5W   
   " �6   .�5X      ��  � ��$	 0 5q�6'   5Y      5Z      5[      