
import { Col, Container, Row } from "react-bootstrap";
import AdminHeader from "../../components/adminHeader";
import { UpdatePageForm } from "../../components/updatePage";
interface PageProps {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    params: any;
}

export default async function UpdatePage({ params }: PageProps) {
    const path = (await params).slug?.join("/") || "";

    return (
        <Container>
            <Row>
                <Col sm={9}>
                    <h1>Редактирование страницы: {path}</h1>
                    <UpdatePageForm pageId={path} />
                </Col>
                <AdminHeader />
            </Row>
        </Container>
    );
}
