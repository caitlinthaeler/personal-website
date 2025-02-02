import axios from 'axios';


const apiUrl = import.meta.env.VITE_API_URL;
console.log("json url: ", apiUrl);
export const fetchJson = async (jsonFile) => {
    if (!jsonFile) {
        console.warn('No file specified');
        return null;
    }
    try {
        const encodedUrl = encodeURIComponent(jsonFile);
        //http://localhost:5283/api/json/
        
        
        console.log("before: ", `${apiUrl}/api/json/${encodedUrl}`)
        
        const response = await axios.get(`${apiUrl}/api/json/${encodedUrl}`);
        
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