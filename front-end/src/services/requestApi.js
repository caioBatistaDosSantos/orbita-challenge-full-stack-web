import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3001',
});

export const GetAll = async () => {
  const { data } = await api.get('/students');
  return data;
}

export const Post = async (body) => {
  const { data } = await api.post('/students', body);
  return data;
}

export const Put = async (ra, body) => {
  const { data } = await api.put(`/students/${ra}`, body);
  return data;
}

export const Delete = async (ra) => {
  const { data } = await api.delete(`/students/${ra}`);
  return data;
}
