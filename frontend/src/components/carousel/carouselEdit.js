import React, { useState } from 'react';
import Dropzone from 'react-dropzone';
import Slider from 'react-slick';
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import Loader from '../layout/Loader';
import { useSelector } from 'react-redux';

const CarouselEdit = () => {


  const [files, setFiles] = useState([]);

  const onDrop = (acceptedFiles) => {
    setFiles(
      acceptedFiles.map(file =>
        Object.assign(file, {
          preview: URL.createObjectURL(file),
          name: file.name,
          size: file.size,
          type: file.type
        })
      )
    );
  };

  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1
  };

  return (
    <div>
      <Dropzone onDrop={onDrop}>
        {({ getRootProps, getInputProps }) => (
          <div {...getRootProps()} style={dropzoneStyle}>
            <input {...getInputProps()} />
            <p>Drag 'n' drop some files here, or click to select files</p>
          </div>
        )}
      </Dropzone>

      {files.length > 0 && (
        <Slider {...settings}>
          {files.map((file, index) => (
            <div key={index}>
              <img src={file.preview} alt={file.name} style={imageStyle} />
              <div>
                <p>Name: {file.name}</p>
                <p>Size: {(file.size / 1024).toFixed(2)} KB</p>
                <p>Type: {file.type}</p>
              </div>
            </div>
          ))}
        </Slider>
      )}
    </div>
  );
};

const dropzoneStyle = {
  border: '2px dashed #cccccc',
  borderRadius: '5px',
  padding: '20px',
  textAlign: 'center',
  cursor: 'pointer',
  marginBottom: '20px'
};

const imageStyle = {
  width: '100%',
  height: 'auto',
  maxHeight: '500px'
};

    
  /* const [images, setImages] = useState([
    "https://via.placeholder.com/600x400",
    "https://via.placeholder.com/600x400",
    "https://via.placeholder.com/600x400",
    "https://via.placeholder.com/600x400"
  ]);
  const [newImage, setNewImage] = useState("");

  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1
  };

  const { loading } = useSelector(
    (state) => state.security
  );

  if (loading)
    {
      return (<Loader/>);
    }

  const addImage = (e) => {
    e.preventDefault();
    if (newImage.trim()) {
      setImages([...images, newImage]);
      setNewImage("");
    }
  };

  const removeImage = (index) => {
    setImages(images.filter((_, i) => i !== index));
  };

  

  return (
    <div style={{ width: '50%', margin: 'auto' }}>
      <h2>Editable Carousel</h2>
      <Slider {...settings}>
        {images.map((img, index) => (
          <div key={index}>
            <img src={img} alt={`Slide ${index + 1}`} />
          </div>
        ))}
      </Slider>
      <form onSubmit={addImage} style={{ marginTop: '20px' }}>
        <input
          type="text"
          value={newImage}
          onChange={(e) => setNewImage(e.target.value)}
          placeholder="Enter image URL"
          style={{ width: '80%', padding: '8px' }}
        />
        <button type="submit" style={{ padding: '8px' }}>Add Image</button>
      </form>
      <div style={{ marginTop: '20px' }}>
        <h3>Current Images</h3>
        <ul>
          {images.map((img, index) => (
            <li key={index}>
              {img}
              <button onClick={() => removeImage(index)} style={{ marginLeft: '10px' }}>Remove</button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  ); */


export default CarouselEdit;
