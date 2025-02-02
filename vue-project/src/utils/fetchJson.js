import axios from 'axios';


export const fetchJson = async (jsonFile) => {
    if (!jsonFile) {
        console.warn('No file specified');
    }
    try {
        const encodedUrl = encodeURIComponent(jsonFile);
        //http://localhost:5283/api/json/
        const response = await axios.get(`/api/json/${encodedUrl}`);
        
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