
import axios from "axios";

axios.defaults.baseURL = "http://localhost:5170";
axios.defaults.headers.post["Content-Type"] = "application/json";
axios.defaults.withCredentials = false; // אם אין צורך בעוגיות/אימות

export default {
  getTasks: async () => {
    const result = await axios.get("/items");
    return result.data;
  },

  addTask: async (name) => {
    console.log("addTask", name);
    const result = await axios.post("/items", { name, isComplete: false });
    return result.data;
  },

  setCompleted: async (id, isComplete) => {
    console.log("setCompleted", { id, isComplete });
    const result = await axios.put(`/items/${id}`, { id, isComplete });
    return result.data;
  },

  deleteTask: async (id) => {
    console.log("deleteTask", id);
    await axios.delete(`/items/${id}`);
  },
};




// import axios from 'axios';

// //const apiUrl = "https://localhost:7271"
// const apiUrl = "http://localhost:5170"

// export default {
//   getTasks: async () => {
//     const result = await axios.get(`${apiUrl}/items`)    
//     return result.data;
//   },

//   addTask: async(name)=>{
//     console.log('addTask', name)
//     //TODO
//     return {};
//   },

//   setCompleted: async(id, isComplete)=>{
//     console.log('setCompleted', {id, isComplete})
//     //TODO
//     return {};
//   },

//   deleteTask:async()=>{
//     console.log('deleteTask')
//   }
// };
