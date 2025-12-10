import os
from moviepy import VideoFileClip

scene = "Scene8"

input_folder = "Videos/" + scene
output_folder = "videos_processed/" + scene + "/normal"

TARGET_WIDTH = 640
TARGET_HEIGHT = 360

os.makedirs(output_folder, exist_ok=True)

for filename in os.listdir(input_folder):
    if filename.lower().endswith((".mp4", ".mov", ".avi", ".mkv")):
        input_path = os.path.join(input_folder, filename)
        output_path = os.path.join(output_folder, f"processed_{filename}")

        print(f"Processing: {filename}")

        clip = VideoFileClip(input_path)

        original_fps = clip.fps
        print(f"Original FPS: {original_fps}")

        resized = clip.resized((TARGET_WIDTH, TARGET_HEIGHT))

        resized.write_videofile(output_path, codec="libx264")

        clip.close()
        resized.close()

print("Done! Resolution reduced.")