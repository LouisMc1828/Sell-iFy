import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";
import { delayedTimeout } from "../utilities/DelayedTimeOut";

export const login = createAsyncThunk(
  "user/login",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };
      const { data } = await axios.post(
        `/api/v1/user/login`,
        params,
        requestConfig
      );
      localStorage.setItem("token", data.token);
      await delayedTimeout(1000);
      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const register = createAsyncThunk(
  "user/register",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      };
      const { data } = await axios.post(
        `/api/v1/user/register`,
        params,
        requestConfig
      );
      localStorage.setItem("token", data.token);
      await delayedTimeout(1000);
      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const update = createAsyncThunk(
  "user/update",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      };
      const { data } = await axios.post(
        `/api/v1/user/update`,
        params,
        requestConfig
      );
      localStorage.setItem("token", data.token);
      await delayedTimeout(1000);
      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const loadUser = createAsyncThunk(
  "user/getUser",
  async ({ rejectWithValue }) => {
    try {
      const { data } = await axios.get(`/api/v1/user`);
      localStorage.setItem("token", data.token);
      await delayedTimeout(1000);
      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const updatePassword = createAsyncThunk(
  "user/updatePassword",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };
      const { data } = await axios.put(
        `/api/v1/user/updatepassword`,
        params,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const forgotSendPassword = createAsyncThunk(
  "user/forgotPassword",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };
      const { data } = await axios.post(
        `/api/v1/user/forgotpassword`,
        params,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const resetPassword = createAsyncThunk(
  "user/resetPassword",
  async ({ email, password, confirmPassword, token }, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };

      const request = {
        email,
        password,
        confirmPassword,
        token,
      };

      const { data } = await axios.post(
        `/api/v1/user/resetpassword`,
        request,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);
