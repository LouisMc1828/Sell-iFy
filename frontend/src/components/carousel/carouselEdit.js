import React, { useState } from 'react';
import Slider from 'react-slick';
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import Loader from '../layout/Loader';
import { useSelector } from 'react-redux';

const CarouselEdit = () => {

    
  const [images, setImages] = useState([
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
  );
};

export default CarouselEdit;
