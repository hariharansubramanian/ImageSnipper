import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import { uploadImage } from './utils/Api';

//TODO: get rid of croppedImageUrl, simply set imageSelectedUrl when getting result from backend
class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      file: '',
      imageSelectedUrl: '',
      croppedImageUrl: ''
    };
    this._handleImageChange = this._handleImageChange.bind(this);
    this._handleSubmit = this._handleSubmit.bind(this);
  }

  render() {
    let { imageSelectedUrl } = this.state;
    let { croppedImageUrl } = this.state;

    let $imageSelectedDiv = null;
    let $croppedImagePreviewDiv = null;

    // if image selected from explorer window, show div, else remains null
    if (imageSelectedUrl) {
      $imageSelectedDiv = (<img className="image" src={imageSelectedUrl} height="50%" width="50%" alt="preview" />);
    }

    // if croppedImageUrl returned from backend POST request, show div, else remains null
    if (croppedImageUrl) {
      $croppedImagePreviewDiv = (<img className="image" src={croppedImageUrl} height="50%" width="50%" alt="Cropped Preview" />);
    }

    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <div>
            <form onSubmit={this._handleSubmit}>
              <input type="file" onChange={this._handleImageChange} />
              <button type="submit" disabled={!$imageSelectedDiv} onClick={this._handleSubmit}>Crop Border</button>
            </form>
            <div className="preview">
              <div className="imageSelected">
                {$imageSelectedDiv}
              </div>
              <div className="croppedImage">
                {$croppedImagePreviewDiv}
              </div>
            </div>
          </div>
        </header>
      </div>

    )
  }

  _handleSubmit(e) {
    e.preventDefault();
    // send POST request with formdata containing image
    uploadImage(this.state.file)
      .then(res => {
        // upon success, set croppedImageUrl, and setState to re-render and show div
        this.setState({
          croppedImageUrl: res.data
        });
      })
      .catch(function (error) {
        console.log(error);
      })
  }

  _handleImageChange(e) {
    e.preventDefault();

    let reader = new FileReader();
    let file = e.target.files[0];
    reader.onloadend = () => {
      this.setState({
        file: file,
        imageSelectedUrl: reader.result,
        croppedImageUrl: null // clear any previously previewing cropped images
      });
    }
    reader.readAsDataURL(file)
  }

}
export default App;
