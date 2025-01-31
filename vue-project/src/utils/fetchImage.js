import axios from 'axios';
import placeholderImage from '@/assets/img/placeholder.png';

export const fetchImage = async (imageUrl) => {
    if (!imageUrl) {
        console.warn('No image url specified');
        return placeholderImage;
    }
    try {
        const encodedImage = encodeURIComponent(imageUrl);
        const response = await axios.get(`http://localhost:5283/api/caitlinthaeler/portfolio_content/image/${encodedImage}`);
        
        return response.data.imageUrl;
    } catch (error) {
        console.error("Error fetching image", error);
        return placeholderImage;
    }
};