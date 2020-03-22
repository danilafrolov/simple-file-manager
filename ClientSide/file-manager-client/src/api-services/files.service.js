import Axios from 'axios';

export default {
  getAllFiles() {
    return Axios.get('/GetUserFiles');
  },

  uploadFile(file) {
    var formData = new FormData();
    formData.append('file', file);
    return Axios.post('/UploadFile', formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
  },

  deleteFile(filename) {
    return Axios.get('/DeleteFile', { params: { filename: filename }});
  }
};