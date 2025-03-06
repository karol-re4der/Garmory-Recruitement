# Garmory-Recruitement

A recruitment task for Garmory.

Game starts on inventory screen. Backpack section is hidden until items are received from the server mockup.
![Screenshot_3](https://github.com/user-attachments/assets/2dd35a9b-a082-42d4-bc9c-f83ee0bb06b6)

After items are loaded, the player can move items freely between slots and equip them on a relevant item slot, either by draging or by double clicking. Total character stats are counted and displayed. Individual item stats can also be shown after hovering the item for a short while.
![Screenshot_1](https://github.com/user-attachments/assets/a0f85afd-194f-4c22-93e0-2a92e378876c)

After closing inventory, the game resumes. Inventory can be returned to at any time by clicking 'escape'. The goal is to destroy barrels that spawn every 30 seconds with the attack - left mouse button/space. 
![Screenshot_2](https://github.com/user-attachments/assets/0395590f-2432-46a7-8b35-ef5e2cf1f401)

The game only uses assets provided with the starting project. Most of the settings are grouped in static "Shortcuts" class for time's sake. Next step (if there's no time limit) would be separating settings into some config file. Defence stat is there for visuals only - other two stats affect player damage/movement speed. Other stats provided by server mockup are still saved, but not used.
