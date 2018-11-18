import argparse
import cv2
import numpy as np


def do_work(src_path, dest_path):
    im = cv2.imread(src_path)
    im = crop_black_borders(im)
    cv2.imwrite(dest_path, im)

def crop_black_borders(im):
    # do your work here
    print("doing work here")
    return im

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='Cuts black border of an image given as argument and saves it as another image whose path is also given as argument')
    parser.add_argument("src_path", help="Full path to the source image.")
    parser.add_argument("dest_path", help="Full path to the result image.")
    args = parser.parse_args()
    do_work(args.src_path, args.dest_path)