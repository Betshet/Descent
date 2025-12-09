import os
from moviepy import VideoFileClip
import moviepy.video.fx as vfx
from moviepy.video.VideoClip import ImageClip

input_folder = "videos_processed/Location0/normal"
output_folder = "videos_processed/Location0/reversed"
frames_folder = "frames/Location0"

os.makedirs(output_folder, exist_ok=True)
os.makedirs(frames_folder, exist_ok=True)

for filename in os.listdir(input_folder):
    if filename.lower().endswith((".mp4", ".mov", ".avi", ".mkv")):
        input_path = os.path.join(input_folder, filename)
        output_path = os.path.join(output_folder, f"reversed_{filename}")

        print(f"Processing: {filename}")

        # Load video
        clip = VideoFileClip(input_path)

        # Reverse video
        reversed_clip = vfx.TimeMirror().apply(clip)
        reversed_clip.write_videofile(output_path, codec="libx264")

        # Extract first frame
        frame = clip.get_frame(0)
        frame_filename = os.path.splitext(filename)[0] + "_frame.png"
        frame_path = os.path.join(frames_folder, frame_filename)

        ImageClip(frame).save_frame(frame_path)

        print(f"Saved frame: {frame_path}")

        clip.close()
        reversed_clip.close()

print("Done!")
