import React from 'react';
import Slider from 'react-slick';
import "slick-carousel/slick/slick.css"; 
import "slick-carousel/slick/slick-theme.css";

const Carousel = () => {
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1
  };

  return (
    <div style={{ width: '50%', margin: 'auto' }}>
      <h2>Simple Carousel</h2>
      <Slider {...settings}>
        <div>
          <img src="https://via.placeholder.com/600x400" alt="Slide 1" />
        </div>
        <div>
          <img src="https://via.placeholder.com/600x400" alt="Slide 2" />
        </div>
        <div>
          <img src="https://via.placeholder.com/600x400" alt="Slide 3" />
        </div>
        <div>
          <img src="https://via.placeholder.com/600x400" alt="Slide 4" />
        </div>
      </Slider>
    </div>
  );
};

export default Carousel;
