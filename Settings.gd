extends Control

signal applied
signal cancelled

onready var config:Config=get_node("/root/Config")
onready var panel=$Panel/VBoxContainer/CenterContainer/GridContainer

func _ready():
	panel.get_node("SeedInput").value=config.current_seed
	panel.get_node("CoordXInput").value=config.current_coord.x
	panel.get_node("CoordYInput").value=config.current_coord.y

func _on_Button_pressed():
	config.current_seed=panel.get_node("SeedInput").value
	config.current_coord=Vector2(panel.get_node("CoordXInput").value,panel.get_node("CoordYInput").value)
	emit_signal("applied")


func _on_Background_gui_input(event):
	if event is InputEventMouseButton:
		if event.pressed:
			emit_signal("cancelled")
