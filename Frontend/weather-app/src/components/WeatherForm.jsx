import { Form, Col, Row } from 'react-bootstrap';

export default function WeatherView ({data}) {

    const convertShiiftTimeUtc = (time) => {
        var d = new Date((new Date().getTime())-time*1000)
        return d.toISOString();
    }

    if(!data.showData){
        return <p>No Data To Display</p>
    }

    const content = data.data;

    return (
        <Form>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    City
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.city} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Location
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.location} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Time
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={convertShiiftTimeUtc(content.time)} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Wind(meter/sec)
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.wind} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Visibility (meter)
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.visibility} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Sky Condition
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.skyCondition} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Temperature in Celcius
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.temperatureCelcius} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Temperature in Fahrenheit
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.temperatureFahrenheit} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Dew Point
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.dewPoint} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Relative Humidity (%)
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.humidity} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">
                    Pressure
                </Form.Label>
                <Col sm="10">
                <Form.Control plaintext readOnly defaultValue={content.pressure} />
                </Col>
            </Form.Group>
        </Form>
    );
}