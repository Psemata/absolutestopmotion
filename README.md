# **Augmented Stop Motion – The AR Animation Lab**  

🚀 **Augmented Stop Motion** is an interactive installation that merges the magic of **stop-motion animation** with **augmented reality (AR)**. This project allows users to create frame-by-frame animations by placing physical **cards** on a table, which are then scanned in real-time by a tablet to generate AR content. The result is a **seamless and intuitive stop-motion experience**, blending **physical interaction** with **digital creativity**.  

---

## 🎥 **How It Works**  
1. **Set up the scene** – Place **Character, Background, Action, or Effect** cards on the table.  
2. **Scan in real-time** – The tablet detects the cards and displays AR elements corresponding to their position.  
3. **Animate step by step** – Move the cards **frame by frame** while the system captures each movement.  
4. **Generate the animation** – The software **compiles the frames** into a smooth stop-motion video.  
5. **Apply effects & export** – Users can add filters (hand-drawn, pixel art, VHS) and **download or share** their animation instantly.  

---

## 🛠 **Technology Stack**  
- **Frontend:** Unity + ARKit (iOS) / ARCore (Android)  
- **Backend:** Custom image recognition & motion tracking  
- **Hardware:** Tablet (iPad / Android), printed NFC / QR-based cards  
- **Processing:** OpenCV for card detection & frame-by-frame analysis  
- **Output:** MP4 video export with creative filters  

---

## 🃏 **Card System**  
| **Card Type**  | **Function**  | **Examples**  |  
|---------------|-------------|-------------|  
| 🎭 **Characters** | Adds animated figures | Human, animal, robot |  
| 🏠 **Backgrounds** | Sets the scene | Forest, city, space |  
| 🎬 **Actions** | Triggers movements | Jump, run, disappear |  
| 🔊 **Sound Effects** | Adds audio layers | Footsteps, explosion |  
| 🎇 **Visual FX** | Enhances visuals | Fire, rain, glow |  

By combining different cards, users can create their own **unique AR-powered stop-motion story**.  

---

## 🎨 **Why It Works**  
✅ **Bridges traditional stop-motion & new media**  
✅ **Accessible for all skill levels** – No prior animation experience needed  
✅ **Hands-on creativity** – Encourages storytelling through tangible interaction  
✅ **Instant feedback & sharing** – Users see their animation evolve in real-time  

---

## 🔧 **Installation & Setup**  
### **1. Clone the Repository**  
```bash
git clone https://github.com/yourusername/augmented-stop-motion.git
cd augmented-stop-motion
```
### **2. Install Dependencies**  
```bash
npm install  # or yarn install (for frontend dependencies)
pip install -r requirements.txt  # if using Python backend
```
### **3. Run the Project**  
```bash
npm run dev  # Starts the AR interface
python server.py  # Launches backend processing (if applicable)
```

---

## 📜 **License**  
This project is licensed under the **HEAD Geneva**
