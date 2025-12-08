import os
from moviepy import VideoFileClip

input_folder = "Videos/Location0"
output_folder = "videos_processed/Location0"
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
        reversed_clip = clip.fx(lambda c: c.fx("time_mirror"))
        reversed_clip.write_videofile(output_path, codec="libx264")

        # Extract last frame of the reversed clip
        # This is equivalent to the first frame of the original video
        first_frame = clip.get_frame(0)
        frame_filename = os.path.splitext(filename)[0] + "_frame.png"
        frame_path = os.path.join(frames_folder, frame_filename)

        # Save PNG
        from moviepy.video.io.ImageSequenceClip import ImageClip
        ImageClip(first_frame).save_frame(frame_path)

        print(f"Saved frame: {frame_path}")

        clip.close()
        reversed_clip.close()

print("Done! Videos reversed and frames saved.")