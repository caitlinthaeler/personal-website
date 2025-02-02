import axios from 'axios';


export const fetchJson = async (jsonFile) => {
    if (!jsonFile) {
        console.warn('No file specified');
    }
    try {
        const encodedUrl = encodeURIComponent(jsonFile);
        console.log(`http://localhost:5283/api/json/${jsonFile}`);
        const response = await axios.get(`http://localhost:5283/api/json/${encodedUrl}`);
        
        if (response.status === 200) {
            console.log(response.data);
            return response.data;
        } else {
            return null; // Fallback to placeholder if no valid response
        }
    } catch (error) {
        console.error("Error fetching json", error);
        return null;
    }
};