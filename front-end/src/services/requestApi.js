import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:3001',
});

export const GetAll = async () => {
  const {data} = await api.get('/Students');
  return data;
}

export const Post = async (body) => {
  const { data } = await api.post('/Students', body);
  return data;
}

export const Put = async (ra, body) => {
  const { data } = await api.put(`/Students/${ra}`, body);
  return data;
}

export const Delete = async (ra) => {
  const { data } = await api.delete(`/Students/${ra}`);
  return data;
}
