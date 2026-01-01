'use client'
import { useState } from "react";
import { Button, Card, CardContent, Typography, Box, Stack } from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";

export default function Home() {
  const [count, setCount] = useState(0);

  return (
    <Box sx={{ p: 4, maxWidth: 800, mx: "auto" }}>
      <Typography variant="h3" component="h1" gutterBottom>
        ✅ Quiz des Capitales - Setup Test
      </Typography>

      <Stack spacing={3} sx={{ mt: 4 }}>
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              <CheckCircleIcon sx={{ verticalAlign: "middle", mr: 1, color: "success.main" }} />
              Next.js 15 App Router
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Le routing fonctionne correctement
            </Typography>
          </CardContent>
        </Card>

        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              <CheckCircleIcon sx={{ verticalAlign: "middle", mr: 1, color: "success.main" }} />
              Material UI v6
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Composants, icônes et thème chargés
            </Typography>
          </CardContent>
        </Card>

        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              <CheckCircleIcon sx={{ verticalAlign: "middle", mr: 1, color: "success.main" }} />
              React 19 Hooks
            </Typography>
            <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
              useState fonctionne : {count} clics
            </Typography>
            <Button
              variant="contained"
              onClick={() => setCount(count + 1)}
            >
              Tester useState
            </Button>
          </CardContent>
        </Card>

        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              <CheckCircleIcon sx={{ verticalAlign: "middle", mr: 1, color: "success.main" }} />
              TypeScript 5
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Types vérifiés à la compilation
            </Typography>
          </CardContent>
        </Card>

        <Card sx={{ bgcolor: "primary.main", color: "primary.contrastText" }}>
          <CardContent>
            <Typography variant="h5" gutterBottom>
              🎉 Tout est prêt !
            </Typography>
            <Typography variant="body1">
              Vous pouvez commencer l'exercice du Quiz des Capitales.
            </Typography>
          </CardContent>
        </Card>
      </Stack>
    </Box>
  );
}
