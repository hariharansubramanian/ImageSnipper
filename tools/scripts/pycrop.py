import argparse
import cv2
import numpy as np


def do_work(src_path, dest_path):
    image = cv2.imread(src_path)  # reads original image from src_path
    image = crop_black_borders(image, 0)  # crop black borders using a tolerance value
    cv2.imwrite(dest_path, image)  # creates cropped image into dest_path
    print('Cropped image saved in: ' + dest_path)


def crop_black_borders(image, tolerance):
    if len(image.shape) == 3:
        flat_img = np.max(image, 2)
    else:
        flat_img = image
    assert len(flat_img.shape) == 2

    rows = np.where(np.max(flat_img, 0) > tolerance)[0]
    if rows.size:
        cols = np.where(np.max(flat_img, 1) > tolerance)[0]
        image = image[cols[0]: cols[-1] + 1, rows[0]: rows[-1] + 1]
    else:
        image = image[:1, :1]

    return image


if __name__ == "__main__":
    parser = argparse.ArgumentParser(
        description='Cuts black border of an image given as argument and saves it as another image whose path is also given as argument')
    parser.add_argument("src_path", help="Full path to the source image.")
    parser.add_argument("dest_path", help="Full path to the result image.")
    args = parser.parse_args()
    do_work(args.src_path, args.dest_path)
