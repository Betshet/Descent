import os
from moviepy import VideoFileClip

scene = "Scene8"

input_folder = "videos_processed/"+ scene +"/normal"
output_folder = "videos_processed/"+ scene +"/final"

TARGET_FPS = 24

os.makedirs(output_folder, exist_ok=True)

for filename in os.listdir(input_folder):
    if filename.lower().endswith((".mp4", ".mov", ".avi", ".mkv")):
        input_path = os.path.join(input_folder, filename)
        output_path = os.path.join(output_folder, f"reduced_{filename}")

        print(f"Processing: {filename}")

        clip = VideoFileClip(input_path)

        original_fps = clip.fps
        print(f"Original FPS: {original_fps}")

        resized = clip.with_fps(TARGET_FPS)

        resized.write_videofile(output_path, codec="libx264")

        clip.close()
        resized.close()

print("Done! FPS reduced.")