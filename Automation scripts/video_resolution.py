import os
from moviepy.editor import VideoFileClip

input_folder = "Videos\Location0"
output_folder = "videos_processed\Location0"

TARGET_WIDTH = 640
TARGET_HEIGHT = 360
TARGET_FPS = 15

os.makedirs(output_folder, exist_ok=True)

for filename in os.listdir(input_folder):
    if filename.lower().endswith((".mp4", ".mov", ".avi", ".mkv")):
        input_path = os.path.join(input_folder, filename)
        output_path = os.path.join(output_folder, f"processed_{filename}")

        print(f"Processing: {filename}")

        clip = VideoFileClip(input_path)

        original_fps = clip.fps
        print(f"Original FPS: {original_fps}")

        resized = clip.resize((TARGET_WIDTH, TARGET_HEIGHT)).set_fps(TARGET_FPS)

        resized.write_videofile(output_path, codec="libx264")

        clip.close()
        resized.close()

print("Done! Resolution + FPS reduced.")