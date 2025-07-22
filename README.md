# 🌀 MoverPhysics – Physics-Based Character Controller for Unity

A physics-driven character movement script built using Unity’s `Rigidbody` and force-based control system. It supports grounded checks, jump impulses, air control, and hit-based force suppression.
Script name is "MoverPhysics.cs".

---

## 🎮 Features

- ✅ Smooth physics-based movement using acceleration and velocity capping  
- ⬆️ Jump support with impulse force  
- ⛰️ Ground check using `SphereCast` with slope limit  
- 💨 Adjustable drag and hit damping  
- 🎛️ Control over air movement  
- 🧪 Debug mode for development & tuning  

---

## 📂 Setup

1. Create a **Rigidbody** object in Unity and attach the `MoverPhysics.cs` script.
2. Ensure the Rigidbody's `interpolate` is on and `rotation` is frozen (to prevent undesired spinning).
3. Tag any object that should **block movement** with `"Hitter"` for the hit damping to take effect.
4. Assign values in the inspector or use the default serialized values.

---

## 🧠 How It Works

### Movement
- Uses force (`AddForce`) to accelerate towards target velocity.
- Automatically damps force when hitting specific objects (`Hitter` tag).
- Capped by `Player_Velocity` via drag simulation.

### Jumping
- Checks if grounded using `SphereCast`.
- Applies vertical impulse on `Jump` input.

### Hit Handling
- On collision with a `"Hitter"` object, applies damping to restrict movement.
- Regains control once velocity drops below threshold.

---

## ⚙️ Parameters

| Variable                  | Description |
|--------------------------|-------------|
| `Player_Velocity`        | Max horizontal speed |
| `Player_Move_Acceleration` | Ground movement force |
| `Player_Jump_Acceleration` | Jump impulse force |
| `groundCheckDistance`    | Distance to check for ground |
| `slopeLimit`             | Max walkable slope angle |
| `hitDamping`             | Damping after colliding with a `"Hitter"` |
| `airControl`             | Horizontal control when airborne |
| `hitControlThreshold`    | Speed below which control is regained |
| `DebugMode`              | Enables detailed logging |

---

## ⌨️ Controls

- `WASD` / `Arrow Keys`: Move character
- `Spacebar`: Jump (only when grounded)

---

## 🧪 Debug Mode

Enable the `DebugMode` checkbox in the inspector to log:
- Current velocity
- Grounded state
- Collision hit state
- Force constants
- Input direction

---

## 🔧 Dependencies

- Unity (Tested on 6.1+)
- RigidBody physics enabled on GameObject
- No external libraries

---

## 🛠️ To Do

- Add crouch / sprint states
- Slope sliding support
- Animation hooks

---

## 📄 License

MIT – Free to use, modify, and distribute. Just give credit!

---

## ✏️ Author

**Shubham Kunkerkar**  
[LinkedIn](https://linkedin.com/in/shubhamkunkerkar) • [GitHub](https://github.com/ShubhamKunkerkar)

