class_name SpeciesList
extends Resource

@export var species: Array[Species]

func at(index: int) -> Species:
	return species[index]

func size() -> int:
	return species.size()