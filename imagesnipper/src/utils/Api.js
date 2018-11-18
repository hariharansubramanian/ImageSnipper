import axios from 'axios';

const BASE_URL = "http://localhost:51421";

export const uploadImage = (file) => {
    var formData = new FormData();
    formData.append("File",file)
    return axios.post(`${BASE_URL}/api/images/`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' }
    });
  

}
