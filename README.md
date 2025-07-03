## GAIA
The GAIA Project aims to explore the cognitive and emotional impact of virtual reality (VR) in the context of workshops designed to raise awareness about environmental issues. Users are immersed in various environments where they experience the direct consequences of their decisions. An ecological score is calculated based on their actions, which influences the state of the environment shown at the end of the experience.

The final scene offers a forward-looking vision: an urban environment that reflects the potential consequences if an entire population had made the same choices as the user. The elements that make up this scene vary in both quantity and quality, depending on the decisions made throughout the experience.
![01](https://github.com/user-attachments/assets/8559ac0a-92cd-45bc-9495-3e4ef1bf7763)
https://youtu.be/eiEXI6rPZPQ

## Transport Workshop

In this workshop, players design a vehicle by selecting three components from a set of available options. They interact with parts laid out on tables, examine and compare them based on their features, and then choose the ones they want to include in their vehicle.  
Each component comes with its own financial cost and carbon footprint, resulting in a total of 27 possible vehicle combinations.
![04](https://github.com/user-attachments/assets/2eb7ffe3-0a24-40e4-92a2-b088a712dda1)

## Food Workshop

In this workshop, players must assemble five meals by choosing from five different types of meat available in a refrigerator. For each dish, they select the meat they want to use, cut it into smaller portions with a knife, weigh it using a scale, and place it on a plate as they see fit.  
Each type of meat carries both a financial and ecological cost that must be taken into account.
![05](https://github.com/user-attachments/assets/d41ed5ef-d67b-4cbe-a069-2a7736dc5428)

## Specifications

- Compatible with: Meta Quest 3 (other VR headsets may work)
- Standalone mode: Not supported
- Engine: Unity 2022.3.23f1
- Render pipeline: URP 14.0.10
- Toolkit: XR Interaction Toolkit

## Controls

- Users can grab objects (such as car components or food items) using the grip and trigger buttons on the controller.  
- Users can teleport by pointing at a teleportation anchor with the thumbstick; teleportation occurs upon release.

## Setting

Car data can be modified in Assets/Gaia/Data/Components (ScriptableObjects).
Meat data can be modified via the GameManager script on the Manager GameObject in the Kitchen scene (values are per gram).
