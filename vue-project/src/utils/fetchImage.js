import axios from 'axios';
import placeholderImage from '@/assets/img/placeholder.png';

export const fetchImage = async (filePath) => {
    if (!filePath) {
        console.warn('No image url specified');
        return placeholderImage;
    }
    try {
        const encodedImage = encodeURIComponent(filePath);
        const response = await axios.get(`http://localhost:5283/api/image/${encodedImage}`,{
            responseType: 'arraybuffer'
        });

        if (response.status === 200) {
            console.log(response);
            const blob = new Blob([response.data], { type: response.headers['content-type'] });


            // Ensure it's a valid image blob before creating an object URL
            if (!blob.type.startsWith("image/")) {
                throw new Error("Invalid image type");
            }
            const imageUrl = URL.createObjectURL(blob);
            return imageUrl;
        } else {
            return placeholderImage; // Fallback to placeholder if no valid response
        }
    } catch (error) {
        console.error("Error fetching image", error);
        return placeholderImage;
    }
};