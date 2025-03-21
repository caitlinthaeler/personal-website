import axios from 'axios';


const apiUrl = import.meta.env.VITE_API_URL;
console.log("json url: ", apiUrl);
export const requestAuthorization = async (username, password) => {
    try {
        //http://localhost:5283/api/auth/login
        const response = await axios.post(`${apiUrl}/api/auth/login`, 
            {username, password},
            {withCredentials: true}
        );
        localStorage.setItem('authToken', response.data.token)
        return true;
    } catch (error) {
        console.error("Login failed:", error.message);
        return false;
    }
};

export const validateToken = async () => {
    try {
        const token = localStorage.getItem('authToken');
        const response = await axios.get(`${apiUrl}/api/auth/validate`, {   
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (response.data.message === 'Token is valid') {
            return true;
        }
        return false;
    } catch (error) {
        console.error("Token validation failed:", error.message);
        return false;
    }
}

export const removeAuthorization = async () => {
    try {
        //http://localhost:5283/api/auth/login
        const response = await axios.post(`${apiUrl}/api/auth/logout`,
            {},
            {withCredentials: true}
        );
        localStorage.removeItem('authToken');
        return true;
    } catch (error) {
        console.error("Logout failed:", error.message);
        return false;
    }
};